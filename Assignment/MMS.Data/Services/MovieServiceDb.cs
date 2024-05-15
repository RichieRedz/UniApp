using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using MMS.Data.Entities;
using MMS.Data.Repository;

namespace MMS.Data.Services;

// EntityFramework Implementation of IMovieService
public class MovieServiceDb : IMovieService
{
    private readonly DataContext db;

    public MovieServiceDb()
    {
        db = new DataContext();
    }

    public void Initialise()
    {
        db.Initialise(); // recreate database
    }

    //----------------------Movie Management----------------------//

public List<Movie> displayMovieCollection()
{
    foreach (var movie in db.Movies)
    {
        calculateAverageRating(movie.MovieId);
    }
    return db.Movies.ToList();
}

public List<string> getAllMovieTitles()
{
    var titles = db.Movies.Select(m => m.Title).ToList();

    return titles;
}
public Movie createNewMovie(Movie m){
    //Check if movie with title exists
    var title = getMovieByTitle(m.Title);
    if (title != null)
    {
        return null;//Movie exists with that title
    }

    //Check if photo url is empty, if so set default image
    string photoUrl = string.IsNullOrWhiteSpace(m.PhotoURL) ? "https://i.ibb.co/Pwk2bBp/Red-and-Blue-Movie-Night-Poster.png" : m.PhotoURL;

    //create new movie
    var movie = new Movie
    {
        Title = m.Title,
        MovieGenre = m.MovieGenre,
        Year = m.Year,
        PhotoURL = photoUrl
    };
    db.Movies.Add(movie);
    db.SaveChanges();

    return movie;
}
public Movie getMovieByTitle(string title)
{
    return db.Movies.FirstOrDefault(m => m.Title != null && m.Title.ToLower() == title.ToLower());
}
public Movie getMovieById(int id)
{
     return db.Movies
            .Include(s => s.Reviews)
            .FirstOrDefault(t => t.MovieId == id);
}
public Movie editMovie(Movie m)
{
    //Check the movie exists
    var movie = getMovieById(m.MovieId);
    if(movie == null)
    {
        return null; //Movie ID not found
    }
    //Check if the updated title is different from the current title
    if(!movie.Title.Equals(m.Title))
    {
        //Check if the new title already exists in the db
        var checkTitleExists = getMovieByTitle(m.Title);
        if(checkTitleExists != null)
        {
            return null; //New title already exists
        }
    }
    //Update available values for the selected movie
    movie.Title = m.Title;
    movie.MovieGenre = m.MovieGenre;
    movie.PhotoURL = m.PhotoURL;
    movie.Year = m.Year;
    //Save changes
    db.SaveChanges();

    return movie;
}
public bool deleteMovieById(int id)
{
    //delete movie and delete reviews
    var s = getMovieById(id);
    if (s == null)
    {
        return false;
    }
    db.Movies.Remove(s);
    // db.Reviews.RemoveRange(s.Reviews);

    db.SaveChanges();
    return true;

}
public List<Movie> GetHighestRankingMovies()
{
    // Retrieve top 5 highest ranked movies from the database
    var topRatedMovies = db.Movies
        .OrderByDescending(m => m.AvgReview)
        .Take(5)
        .ToList();

    return topRatedMovies.ToList();
}
public IList<Movie> SearchMovies(string query) 
    {
        // ensure query is not null and convert to lowercase    
        query = query == null ? "" : query.ToLower();

        // Convert enum to string for comparison
        string genreString = query;

        //Search for Movies by Title or Genre
        return db.Movies
                //  .Include(t => t.Reviews)
                 .OrderBy(t => t.MovieId)
                 .AsEnumerable() // Switch to client-side evaluation
                 .Where(t => t.Title.ToLower().Contains(query) || 
                         t.MovieGenre.ToString().ToLower().Contains(query))
                .ToList();
    }


// //----------------------Movie Review Management----------------------//
public Review addReviewOnCurrentMovie(Review r)
{
    //Check movie id
    var movieCheck = getMovieById(r.MovieId);
    if (movieCheck != null)
    {
        var review = new Review
            {
                ReviewAuthor = r.ReviewAuthor,
                Rating = r.Rating,
                ReviewText = r.ReviewText,
                Date = DateTime.Now,
                MovieId = r.MovieId // Set the MovieId for the review
            };
    
    //add review to db and save it
    db.Reviews.Add(review);
    db.SaveChanges();

    //send to have the review recalculated for the movie. This again saves in the method.
    calculateAverageRating(r.MovieId);
    return review;
    }
        else
    {
        // Movie does not exist, return null
        return null;
    }
}
// Review deleteReviewForCurrentMovie();
public void calculateAverageRating(int movieId)
{
     var movie = db.Movies
            .Include(m => m.Reviews)
            .FirstOrDefault(m => m.MovieId == movieId);

        if (movie != null)
        {
            
            if (movie.Reviews.Any())
            {
                movie.AvgReview = movie.Reviews.Average(r => r.Rating);
            }
            else
            {
                movie.AvgReview = -1; // Set default to -1 to change to a - in the view when no review is present
            }
            db.SaveChanges();
        }
}

public List<Review> getReviewList()
{
    foreach (var review in db.Reviews)
    {
        calculateAverageRating(review.MovieId);
    }
    return db.Reviews.ToList();
}

public Review GetReviewById(int id)
{
    return db.Reviews.FirstOrDefault(r => r.Id == id);
}
public Review EditReview(Review review)
{
    // Check if the review exists
    var existingReview = GetReviewById(review.Id);

    if (existingReview == null)
    {
        return null; // Review ID not found
    }

    // Update available values for the selected review
    existingReview.ReviewAuthor = review.ReviewAuthor;
    existingReview.Rating = review.Rating;
    existingReview.ReviewText = review.ReviewText;
    existingReview.Date = DateTime.Now;
    // existingReview.MovieId = review.MovieId;

    calculateAverageRating(existingReview.MovieId);

    // Save changes
    db.SaveChanges();

    return existingReview;
}

}