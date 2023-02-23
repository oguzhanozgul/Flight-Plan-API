using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_rpg.Data
{
  public class AuthRepository : IAuthRepository
  {
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;

    public AuthRepository(DataContext context, IConfiguration configuration)
    {
      _context = context;
      _configuration = configuration;
    }
    public async Task<ServiceResponse<string>> Login(string username, string passsword)
    {
      var serviceResponse = new ServiceResponse<string>();
      var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));

      if (user is null)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = "Invalid username or password.";
        return serviceResponse;
      }

      if (!VerifyPasswordHash(passsword, user.PasswordHash, user.PasswordSalt))
      {
        serviceResponse.Success = false;
        serviceResponse.Message = "Invalid username or password.";
        return serviceResponse;
      }

      serviceResponse.Data = CreateToken(user);
      return serviceResponse;
    }

    public async Task<ServiceResponse<int>> Register(User user, string passsword)
    {
      var serviceResponse = new ServiceResponse<int>();

      if (await UserExists(user.Username))
      {
        serviceResponse.Success = false;
        serviceResponse.Message = "User already exists.";
        return serviceResponse;
      }

      CreatePasswordHash(passsword, out var passwordHash, out var passwordSalt);
      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      _context.Users.Add(user);
      await _context.SaveChangesAsync();
      serviceResponse.Data = user.Id;
      return serviceResponse;
    }

    public async Task<bool> UserExists(string username)
    {
      if (await _context.Users.AnyAsync(user => user.Username.ToLower() == username.ToLower()))
      {
        return true;
      }
      return false;
    }

    private void CreatePasswordHash(string passsword, out byte[] passwordHash, out byte[] passwordSalt)
    {
      using var hmac = new System.Security.Cryptography.HMACSHA512();
      passwordSalt = hmac.Key;
      passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passsword));
    }

    private bool VerifyPasswordHash(string passsword, byte[] passwordHash, byte[] passwordSalt)
    {
      using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
      var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passsword));
      return computedHash.SequenceEqual(passwordHash);
    }

    private string CreateToken(User user)
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
      };

      var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
      if (appSettingsToken is null)
      {
        throw new Exception("AppSettings Token is null");
      }

      var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = credentials,
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}
