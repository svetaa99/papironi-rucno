using Microsoft.EntityFrameworkCore;
using LibraryService.Models;

namespace LibraryService.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
      
    }

    public DbSet<PublishedPaper> PublishedPapers { get; set;}

  }
}