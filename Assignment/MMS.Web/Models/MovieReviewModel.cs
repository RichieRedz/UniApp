using MMS.Data.Entities;

namespace MMS.Web.Models;

public class MovieReviewViewModel
{
    public Movie Movie { get; set; }
    public Review Review { get; set; }
    public List<Review> AllReviews { get; set; }
}