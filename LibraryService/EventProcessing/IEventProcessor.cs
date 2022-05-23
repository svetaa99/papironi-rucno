namespace LibraryService.EventProcessing
{
  public interface IEventProcessor
  {
    void ProcessEvent(string message);
  }
}