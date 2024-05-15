using System.ComponentModel.DataAnnotations;

namespace MMS.Data.Entities;
public class Review
{     
    public int Id { get; set; }
    public string ReviewAuthor { get; set; }
    [Required][Range (1,5)]
    public int Rating { get; set; }
    public string ReviewText { get; set; }
    public DateTime Date { get; set; }
    public int MovieId { get; set; } // Foreign key to Movie
    public Movie Movie { get; set; } // Navigation property for Movie
    
}
