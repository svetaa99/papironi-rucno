using System;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using UserService;

namespace SciPaperService.Grpc
{
  public class UserDataClient : IUserDataClient
  {
    private readonly IConfiguration _configuration;

    public UserDataClient(IConfiguration configuration) 
    {
      _configuration = configuration;
      
    }
    public string GetAuthorName(string jwt)
    {
      Console.WriteLine($"Calling API {_configuration["UserService"]}");
      var channel = GrpcChannel.ForAddress(_configuration["UserService"]);
      var client = new GrpcUser.GrpcUserClient(channel);
      var request = new GetAuthorNameRequest();
      request.Jwt = jwt;

      try {
        var response = client.GetAuthorName(request);
        return response.Author;
      }
      catch (Exception ex)
      {
        Console.WriteLine("Could not call REMOTE API");
        Console.WriteLine(ex.Message);
        return null;
      }
    }

    public bool IsLoggedIn(string jwt)
    {
      Console.WriteLine($"Calling LOGGED IN API {_configuration["UserService"]}");
      var channel = GrpcChannel.ForAddress(_configuration["UserService"]);
      var client = new GrpcUser.GrpcUserClient(channel);
      var request = new IsLoggedInRequest();
      request.Jwt = jwt;

      try {
        var response = client.IsLoggedIn(request);
        return response.IsLoggedIn;
      }
      catch (Exception ex)
      {
        Console.WriteLine("Could not call REMOTE API");
        Console.WriteLine(ex.Message);
        return false;
      }
    }
  }
}