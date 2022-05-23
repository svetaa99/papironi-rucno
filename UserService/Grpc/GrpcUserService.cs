using System;
using System.Threading.Tasks;
using Grpc.Core;
using UserService.Data;
using UserService.Models;
using UserService.Services;

namespace UserService.Grpc
{
  public class GrpcUserService : GrpcUser.GrpcUserBase
  {
    private readonly IUserEntityService _service;

    public GrpcUserService(IUserEntityService service)
    {
      _service = service;
    }

    public override Task<IsLoggedInResponse> IsLoggedIn(IsLoggedInRequest request, ServerCallContext context)
    {
      var response = new IsLoggedInResponse();
      var isLoggedIn = _service.IsLoggedIn(request.Jwt);

      response.IsLoggedIn = isLoggedIn;

      return Task.FromResult(response);
    }

    public override Task<GetAuthorNameResponse> GetAuthorName(GetAuthorNameRequest request, ServerCallContext context)
    {
      var response = new GetAuthorNameResponse();
      User user = _service.GetLoggedUser(request.Jwt);
      response.Author = user == null ? "" : $"{user.Firstname} {user.Lastname}";

      return Task.FromResult(response);
    }
  }
}