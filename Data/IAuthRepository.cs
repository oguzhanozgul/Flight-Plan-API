using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Data
{
  public interface IAuthRepository
  {
    Task<ServiceResponse<int>> Register(User user, string passsword);
    Task<ServiceResponse<string>> Login(string username, string passsword);
    Task<bool> UserExists(string username); // no need to return a ServiceResponse since we use this internally and don't send it to the client.
  }
}
