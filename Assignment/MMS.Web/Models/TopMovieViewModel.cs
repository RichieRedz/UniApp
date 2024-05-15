
namespace MMS.Web.Models;

public class TopMovieViewModel
{
    public List<TopMovieViewModel> TopRatedMovies { get; set; }
    public string Title { get; set; }
    public string PhotoURL { get; set; }
    public double Rating { get; set; }
    public int MovieId { get; set; }
}