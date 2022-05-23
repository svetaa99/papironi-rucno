using System.Collections.Generic;
using SciPaperService.Models;

namespace SciPaperService.Data
{
  public interface IPaperRepository
  {
    bool SaveChanges();
    IEnumerable<Paper> GetAllPapers();
    Paper GetPaperById(int id);
    void CreatePaper(Paper paper);
    Paper UpdatePaper(Paper paper);
    void DeletePaper(int id);
  }
}