using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SciPaperService.Data;
using SciPaperService.Grpc;
using SciPaperService.MessageBus;
using SciPaperService.Models;
using SciPaperService.Models.Dtos;

namespace SciPaperService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PapersController : ControllerBase
  {
    private readonly IPaperRepository _repository;
    private readonly IUserDataClient _userClient;
    private readonly IMessageBusClient _messageBusClient;

    public PapersController(IPaperRepository paperRepository, IUserDataClient userClient, IMessageBusClient bus)
    {
      _repository = paperRepository;
      _userClient = userClient;
      _messageBusClient = bus;
    }

    [HttpGet("publish/{paperId}")]
    public ActionResult<PublishedPaperDTO> PublishPaper(int paperId)
    {
      Paper paper = _repository.GetPaperById(paperId);
      if (paper == null)
      {
        return NotFound();
      }

      var paperDto = new PublishedPaperDTO() {Author = paper.Author, Title = paper.Title, PaperId = paperId, Event = ""};

      try
      {
        paperDto.Event = "PUBLISH_PAPER";
        _messageBusClient.PublishNewPaper(paperDto);

      }
      catch (Exception ex)
      {
        Console.WriteLine($"Could not send message from controller. Error: {ex.Message}");
      }

      return Created(nameof(paperDto), paperDto);
    }

    [HttpPost]
    public ActionResult<Paper> createPaper(Paper paper)
    {
      var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last() ?? "";

      bool isLoggedIn = _userClient.IsLoggedIn(token);
      if (isLoggedIn)
      {
        string author = _userClient.GetAuthorName(token);
        paper.Author = author;
        _repository.CreatePaper(paper);
        return Ok(paper);
      }
      else
      {
        return Unauthorized();
      }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Paper>> getPapers()
    {
      var paperItem = _repository.GetAllPapers();

      return Ok(paperItem);
    }

    [HttpGet("{id}")]
    public ActionResult<Paper> getPaperById(int paperId)
    {
      var paperItem = _repository.GetPaperById(paperId);
      Console.WriteLine(paperItem);
      return Ok(paperItem);
    }

    [HttpDelete("{id}")]
    public ActionResult deletePaper(int id)
    {
      _repository.DeletePaper(id);

      return Ok();
    }

    [HttpGet("getName")]
    public ActionResult<string> GetAuthor()
    {
      var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
      if (token == null)
      {
        return Ok(_userClient.GetAuthorName(""));
      }
      return Ok(_userClient.GetAuthorName(token));
    }


  }
}