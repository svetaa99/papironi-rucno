using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SciPaperService.Models
{
  public class Paper
  {
    [Key]
    public int Id { get; set; }
    public string Author { get; set; }
    public string Title { get; set; }
    public ICollection<Section> Sections { get; set; }
  }
}