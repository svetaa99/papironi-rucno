using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UserService.Models;

namespace UserService.Data 
{
  public static class PrepDb 
  {
    public static void PrepPopulation(IApplicationBuilder app) 
    {
      using( var serviceScope = app.ApplicationServices.CreateScope())
      {
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
      }
    }

    private static void SeedData(AppDbContext context)
    {
      if (!context.Users.Any())
      {
        Console.WriteLine("---> Seeding data...");

        context.Users.AddRange(
          new User() {Username="sveta", Password="sveta123", Firstname="Svetozar", Lastname="Vulin", Email="sveta@gmail.com"},
          new User() {Username="laza", Password="laza123", Firstname="Laza", Lastname="Lazic", Email="laza@gmail.com"},
          new User() {Username="mika", Password="mika123", Firstname="Mika", Lastname="Mikic", Email="mika@gmail.com"}          
        );

        context.SaveChanges();
      }
      else
      {
        Console.WriteLine("---> We already have data");
      }

    }
  }
}