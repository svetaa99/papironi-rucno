using SciPaperService.Models.Dtos;

namespace SciPaperService.MessageBus 
{
  public interface IMessageBusClient
  {
    void PublishNewPaper(PublishedPaperDTO dto);
  }
}