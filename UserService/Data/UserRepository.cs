using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;

namespace UserService.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) 
    {
      _context = context;
    }

    public void CreateUser(User user)
    {
      if (user == null)
      {
        throw new ArgumentNullException(nameof(user));
      }

      _context.Users.Add(user);

      _context.SaveChanges();
    }

    public IEnumerable<User> GetAllUsers()
    {
      return _context.Users.ToList();
    }

    public User GetUserByUsername(string username)
    {
      return _context.Users.FirstOrDefault(u => u.Username.Equals(username));
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() >= 0);
    }

    public User UpdateUser(User user)
    {
      if (user == null)
      {
        throw new ArgumentNullException(nameof(user));
      }

      var result = _context.Users.SingleOrDefault(u => u.Username == user.Username);
      if (result != null)
      {
          _context.Entry(result).CurrentValues.SetValues(user);
          _context.SaveChanges();
          return result;
      }

      
      throw new KeyNotFoundException();
    }
  }
}