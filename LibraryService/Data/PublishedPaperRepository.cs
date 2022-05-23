using System;
using System.Collections.Generic;
using System.Linq;
using LibraryService.Models;

namespace LibraryService.Data
{
  public class PublishedPaperRepository : IPublishedPaperRepository
  {
    private readonly AppDbContext _context;

    public PublishedPaperRepository(AppDbContext context)
    {
      _context = context;
    }

    public void CreatePublishedPaper(PublishedPaper paper)
    {
      if (paper == null)
      {
        throw new ArgumentNullException(nameof(paper));
      }

      _context.PublishedPapers.Add(paper);

      _context.SaveChanges();
    }

    public IEnumerable<PublishedPaper> GetAllPublishedPapers()
    {
      return _context.PublishedPapers.ToList();
    }

    public PublishedPaper GetPublishedPaperById(int id)
    {
      return _context.PublishedPapers.FirstOrDefault<PublishedPaper>(p => p.Id == id);
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() >= 0);
    }

    public PublishedPaper UpdatePublishedPaper(PublishedPaper paper)
    {
      if (paper == null)
      {
        throw new ArgumentNullException(nameof(paper));
      }

      var result = _context.PublishedPapers.SingleOrDefault(p => p.Id == paper.Id);
      if (result != null)
      {
          _context.Entry(result).CurrentValues.SetValues(paper);
          _context.SaveChanges();
          return result;
      }
      
      throw new KeyNotFoundException();
    }
  }
}