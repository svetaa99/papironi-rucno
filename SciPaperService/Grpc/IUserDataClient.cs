using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SciPaperService.Grpc
{
  public interface IUserDataClient
  {
    bool IsLoggedIn(string jwt);
    string GetAuthorName(string jwt);
  }
}