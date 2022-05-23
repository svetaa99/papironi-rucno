using System;
using System.Text.Json;
using LibraryService.Data;
using LibraryService.Models;
using LibraryService.Models.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryService.EventProcessing
{
  public class EventProcessor : IEventProcessor
  {
    private readonly IServiceScopeFactory _scopeFactory;

    public EventProcessor(IServiceScopeFactory scopeFactory)
    {
      _scopeFactory = scopeFactory;
    }

    public void ProcessEvent(string message)
    {
      var eventType = DetermineEvent(message);

      switch(eventType)
      {
        case EventType.PublishPaper:
          AddPublishedPaper(message);
          break;
        default:
          break;
      }
    }

    private void AddPublishedPaper(string publishedPaperMessage)
    {
      using (var scope = _scopeFactory.CreateScope())
      {
        var repo = scope.ServiceProvider.GetRequiredService<IPublishedPaperRepository>();
        var dto = JsonSerializer.Deserialize<PublishedPaperDto>(publishedPaperMessage);

        try
        {
          PublishedPaper paper = new PublishedPaper() {Author = dto.Author, PaperId = dto.PaperId, PaperTitle = dto.Title};
          repo.CreatePublishedPaper(paper);
          repo.SaveChanges();
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Could not add paper. Error: {ex.Message}");
        }
      }
    }

    private EventType DetermineEvent(string notificationMessage)
    {
      Console.WriteLine("Determining Event Type");

      var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

      switch(eventType.Event)
      {
        case "PUBLISH_PAPER":
          return EventType.PublishPaper;
        default:
          return EventType.Undetermined;
      }
    }
  }

  enum EventType
  {
    PublishPaper,
    Undetermined
  }
}