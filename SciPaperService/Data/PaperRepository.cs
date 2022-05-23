using System;
using System.Collections.Generic;
using System.Linq;
using SciPaperService.Models;

namespace SciPaperService.Data
{
  public class PaperRepository : IPaperRepository
  {
    private readonly AppDbContext _context;

    public PaperRepository(AppDbContext context) 
    {
      _context = context;
    }
    public void CreatePaper(Paper paper)
    {
      if (paper == null)
      {
        throw new ArgumentNullException(nameof(paper));
      }

      _context.Papers.Add(paper);

      _context.SaveChanges();
    }

    public void DeletePaper(int id)
    {
      Paper paper = _context.Papers.Single(p => p.Id == id);
      if (paper != null) 
      {
        _context.Remove(paper);
      }
      _context.SaveChanges();
    }

    public IEnumerable<Paper> GetAllPapers()
    {
      return _context.Papers.ToList();
    }

    public Paper GetPaperById(int id)
    {
      return _context.Papers.FirstOrDefault<Paper>(p => p.Id == id);
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() >= 0);
    }

    public Paper UpdatePaper(Paper paper)
    {
      if (paper == null)
      {
        throw new ArgumentNullException(nameof(paper));
      }

      var result = _context.Papers.SingleOrDefault(p => p.Id == paper.Id);
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