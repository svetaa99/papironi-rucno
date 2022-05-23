using Microsoft.EntityFrameworkCore;
using SciPaperService.Models;

namespace SciPaperService.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
      
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Section>()
            .HasOne<Paper>(s => s.Paper)
            .WithMany(p => p.Sections)
            .HasForeignKey(s => s.PaperId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<Paper> Papers { get; set;}
    public DbSet<Section> Sections {get; set;}
  }
}