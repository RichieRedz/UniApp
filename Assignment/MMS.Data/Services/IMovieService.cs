using System;
using System.Collections.Generic;
	
using MMS.Data.Entities;
	
namespace MMS.Data.Services;

// This interface describes the operations that a MovieService class should implement
public interface IMovieService
{
    void Initialise();
        
    // add suitable method definitions to implement assignment requirements 

//----------------------Movie Management----------------------//
List<Movie> displayMovieCollection();
List<string> getAllMovieTitles();
Movie createNewMovie(Movie m);
Movie getMovieByTitle(string title);
Movie getMovieById(int id);
Movie editMovie(Movie m);
bool deleteMovieById(int id);
List<Movie> GetHighestRankingMovies();
IList<Movie> SearchMovies(string query);


//----------------------Movie Review Management----------------------//
Review addReviewOnCurrentMovie(Review r);
// Review deleteReviewForCurrentMovie();
void calculateAverageRating(int movieId);

List<Review> getReviewList();
Review GetReviewById(int id);
Review EditReview(Review review);
    
}