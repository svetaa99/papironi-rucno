using System.ComponentModel.DataAnnotations;

namespace SciPaperService.Models
{
  public class Section
  {
    [Key]
    public int Id { get; set; }
    public Paper Paper {get; set;}
    public int PaperId {get; set;}
    public string Name { get; set; }
    public string Content { get; set; }
  }
}