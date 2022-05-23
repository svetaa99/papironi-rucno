using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserService.Data;
using UserService.Models;
using UserService.Utils;

namespace UserService.Services
{
  public class UserEntityService : IUserEntityService
  {
    private readonly AppDbContext _context;
    private readonly AppSettings _appSettings;
    private readonly IUserRepository _repository;
    private HttpContext _httpContext;


    public UserEntityService(IUserRepository repo, AppDbContext context, IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
    {
      _context = context;
      _appSettings = appSettings.Value;
      _repository = repo;
      _httpContext = httpContextAccessor.HttpContext;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public void CreateUser(User user)
    {
      _repository.CreateUser(user);
    }

    public IEnumerable<User> GetAllUsers()
    {
      var userItem = _repository.GetAllUsers();
      return userItem;
    }

    public User GetLoggedUser(string jwt)
    {
      var userId = JwtUtils.DestructureJwtToUserId(jwt);
      var user = (User)_repository.GetUserByUsername(userId);
      if (user == null)
      {
        return null;
      }
      
      return user;
    }

    public User GetUserByUsername(string username)
    {
      return _context.Users.FirstOrDefault(u => u.Username.Equals(username));
    }

    public bool IsLoggedIn(string jwt)
    {
      var userId = JwtUtils.DestructureJwtToUserId(jwt);
      var user = (User)_repository.GetUserByUsername(userId);
      if (user == null)
      {
          return false;
      }
      return true;
    }

    public User UpdateUser(User user)
    {
      var updatedUser = _repository.UpdateUser(user);
      if (updatedUser != null) 
      {
        return updatedUser;
      }
      return null;
    }

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Username) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
  }
}