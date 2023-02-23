using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Weapon;

namespace dotnet_rpg.Services.WeaponService
{
  public class WeaponService : IWeaponService
  {
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public WeaponService(DataContext dataContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
      _dataContext = dataContext;
      _httpContextAccessor = httpContextAccessor;
      _mapper = mapper;
    }


    private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
        .FindFirstValue(ClaimTypes.NameIdentifier)!);


    public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
    {
      var response = new ServiceResponse<GetCharacterDto>();
      try
      {
        var character = await _dataContext.Characters
            .Include(c => c.User) // belki de 1:1 iliski oldugundandir, ama bunu yazmasam da calisiyor
            .Where(c => c.User!.Id == GetUserId())
            .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId);

        if (character is null) throw new Exception($"Character with Id {newWeapon.CharacterId} not found.");

        var weapon = new Weapon
        {
          Name = newWeapon.Name,
          Damage = newWeapon.Damage,
          Character = character
        };

        _dataContext.Weapons.Add(weapon);
        await _dataContext.SaveChangesAsync();

        response.Data = _mapper.Map<GetCharacterDto>(character);
      }
      catch (Exception ex)
      {
        response.Success = false;
        response.Message = ex.Message;
      }

      return response;
    }
  }
}
