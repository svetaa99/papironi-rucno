using System.ComponentModel.DataAnnotations;

namespace LibraryService.Models
{
  public class PublishedPaper
  {
    [Key]
    public int Id { get; set; }
    public int PaperId { get; set; }
    public string PaperTitle { get; set; }
    public string Author { get; set; }
  }
}