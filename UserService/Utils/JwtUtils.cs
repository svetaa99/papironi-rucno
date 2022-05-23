using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserService.Models;

namespace UserService.Utils
{
  public static class JwtUtils
  {
    public static string DestructureJwtToUserId(string jwt)
    {
      try
      {
          var tokenHandler = new JwtSecurityTokenHandler();
          var key = Encoding.ASCII.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING");
          tokenHandler.ValidateToken(jwt, new TokenValidationParameters
          {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(key),
              ValidateIssuer = false,
              ValidateAudience = false,
              // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
              ClockSkew = TimeSpan.Zero
          }, out SecurityToken validatedToken);

          var jwtToken = (JwtSecurityToken)validatedToken;
          var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

          // attach user to context on successful jwt validation
          return userId;
      }
      catch
      {
          // do nothing if jwt validation fails
          // user is not attached to context so request won't have access to secure routes
          return null;
      }
    }
  }
}