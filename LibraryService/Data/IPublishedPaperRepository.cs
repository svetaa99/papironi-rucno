using System.Collections.Generic;
using LibraryService.Models;

namespace LibraryService.Data
{
  public interface IPublishedPaperRepository
  {
    bool SaveChanges();
    IEnumerable<PublishedPaper> GetAllPublishedPapers();
    PublishedPaper GetPublishedPaperById(int id);
    void CreatePublishedPaper(PublishedPaper paper);
    PublishedPaper UpdatePublishedPaper(PublishedPaper paper);
  }
}