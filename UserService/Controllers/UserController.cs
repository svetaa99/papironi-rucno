using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.Models;
using UserService.Services;
using UserService.Utils;

namespace UserService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {
    private readonly IUserEntityService _service;

    public UserController(IUserEntityService userService) 
    {
      _service = userService;
    }

    [HttpPost("login")]
    public IActionResult Login(AuthenticateRequest model)
    {
      var user = _service.Authenticate(model);

      if (user == null)
          return BadRequest(new { message = "Username or password is incorrect" });

      return Ok(user);
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
      _service.CreateUser(user);

      return Ok(user);
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> getUsers() 
    {
      var users = _service.GetAllUsers();

      return Ok(users);
    }

    [HttpPost]
    public ActionResult<User> createUser(User user) 
    {
      _service.CreateUser(user);

      return Ok(user);
    }

    [HttpGet("{username}")]
    public ActionResult<User> getUserByUsername(string username)
    {
      var user = _service.GetUserByUsername(username);
      if (user != null) 
      {
        return Ok(user);
      }
      return NotFound();
    }

    [HttpPut]
    public ActionResult<User> updateUser(User user)
    {
      var updatedUser = _service.UpdateUser(user);

      if (updatedUser != null)
      {
        return Ok(updatedUser);
      }

      return NotFound();
    }
  }
}