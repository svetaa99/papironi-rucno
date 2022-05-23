using System.Collections.Generic;
using UserService.Models;

namespace UserService.Data 
{
  public interface IUserRepository 
  {
    bool SaveChanges();
    IEnumerable<User> GetAllUsers();
    User GetUserByUsername(string username);
    void CreateUser(User user);
    User UpdateUser(User user);
  }
}