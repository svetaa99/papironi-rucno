using System.Collections.Generic;
using LibraryService.Data;
using LibraryService.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers 
{
  [ApiController]
  [Route("[controller]")]
  public class PublishedPapersController : ControllerBase
  {
    private readonly IPublishedPaperRepository _repository;

    public PublishedPapersController(IPublishedPaperRepository repo)
    {
      _repository = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PublishedPaper>> getPapers() 
    {
      var paperItem = _repository.GetAllPublishedPapers();

      return Ok(paperItem);
    }

    [HttpPost]
    public ActionResult<PublishedPaper> createPaper(PublishedPaper paper) 
    {
      _repository.CreatePublishedPaper(paper);

      return Ok(paper);
    }
  }
}