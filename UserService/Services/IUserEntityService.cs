using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Services
{
  public interface IUserEntityService
  {
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    User GetUserByUsername(string username);
    IEnumerable<User> GetAllUsers();
    void CreateUser(User user);
    User UpdateUser(User user);
    bool IsLoggedIn(string jwt);
    User GetLoggedUser(string jwt);
  }
}