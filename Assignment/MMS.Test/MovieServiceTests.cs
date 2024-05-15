
using System;
using System.Linq;
using Xunit;

using MMS.Data.Services;
using MMS.Data.Entities;

namespace MMS.Test;
   
// ==================== MovieService Tests =============================
[Collection("Sequential")]
public class MovieServiceTests
{
    private readonly IMovieService svc;

    public MovieServiceTests()
    {
        // general arrangement
        svc = new MovieServiceDb();
        
        // ensure data source is empty before each test
        svc.Initialise();
    }

    // ========================== TBC Recipe Tests  =========================
    
[Fact]
public void DisplayMovieCollection_ShouldReturnAllMovies()
{
    // Arrange
    var m1 = svc.createNewMovie(new Movie { Title = "Movie 1", MovieGenre = Movie.Genre.Action, Year = 2022 });
    var m2 = svc.createNewMovie(new Movie { Title = "Movie 2", MovieGenre = Movie.Genre.Comedy, Year = 2023 });
    var m3 = svc.createNewMovie(new Movie { Title = "Movie 3", MovieGenre = Movie.Genre.Drama, Year = 2024 });

    // Act
    var movies = svc.displayMovieCollection();

    // Assert
    Assert.NotEmpty(movies);
    Assert.Equal(3, movies.Count);
}

[Fact]
public void GetAllMovieTitles_ShouldReturnAllMovieTitles()
{
    // Arrange
    var m1 = svc.createNewMovie(new Movie { Title = "Movie 1", MovieGenre = Movie.Genre.Action, Year = 2022 });
    var m2 = svc.createNewMovie(new Movie { Title = "Movie 2", MovieGenre = Movie.Genre.Comedy, Year = 2023 });
    var m3 = svc.createNewMovie(new Movie { Title = "Movie 3", MovieGenre = Movie.Genre.Drama, Year = 2024 });

    // Act
    var movieTitles = svc.getAllMovieTitles();

    // Assert
    Assert.NotEmpty(movieTitles);
    Assert.Equal(3, movieTitles.Count);
}

[Fact]
public void CreateNewMovie_WithValidMovie_ShouldReturnNewMovie()
{
    // Arrange
    var m1 = svc.createNewMovie (new Movie {
                Title = "The Shawshank Redemption",
                Year = 1994,
                MovieGenre = Movie.Genre.Drama,
                PhotoURL = "https://i.ibb.co/KxmqKj9/Shawshank-Redemption-Movie-Poster.jpg"
            });

    // Act
    var result = svc.getMovieById(m1.MovieId);

    // Assert
    Assert.NotNull(result);
    Assert.Equal("The Shawshank Redemption", result.Title);
    Assert.Equal(1994, result.Year);
    Assert.Equal(Movie.Genre.Drama, result.MovieGenre);
    Assert.Equal("https://i.ibb.co/KxmqKj9/Shawshank-Redemption-Movie-Poster.jpg", result.PhotoURL);
}

[Fact]
public void GetMovieByTitle_WithExistingTitle_ShouldReturnMovie()
{
    // Arrange
    var m1 = svc.createNewMovie (new Movie {
                Title = "The Shawshank Redemption",
                Year = 1994,
                MovieGenre = Movie.Genre.Drama,
                PhotoURL = "https://i.ibb.co/KxmqKj9/Shawshank-Redemption-Movie-Poster.jpg"
            });

    // Act
    var result = svc.getMovieByTitle(m1.Title);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(m1.Title, result.Title);
}

[Fact]
public void GetMovieByTitle_WithNonExistingTitle_ShouldReturnNull()
{
    // Arrange
    svc.createNewMovie (new Movie {
                Title = "The Shawshank Redemption",
                Year = 1994,
                MovieGenre = Movie.Genre.Drama,
                PhotoURL = "https://i.ibb.co/KxmqKj9/Shawshank-Redemption-Movie-Poster.jpg"
            });

    // Act
    var result = svc.getMovieByTitle("No Title");

    // Assert
    Assert.Null(result);
}

[Fact]
public void GetMovieById_ShouldReturnMovie()
{
    // Arrange
    var m1 = svc.createNewMovie(new Movie { Title = "Movie 1", MovieGenre = Movie.Genre.Action, Year = 2022 });
    var m2 = svc.createNewMovie(new Movie { Title = "Movie 2", MovieGenre = Movie.Genre.Comedy, Year = 2023 });
    var m3 = svc.createNewMovie(new Movie { Title = "Movie 3", MovieGenre = Movie.Genre.Drama, Year = 2024 });

    // Act
    var movieId = svc.getMovieById(1);

    // Assert
    Assert.NotNull(movieId);
    Assert.Equal("Movie 1", movieId.Title);
    Assert.Equal(2022, movieId.Year);
    Assert.Equal(Movie.Genre.Action, movieId.MovieGenre);
}

[Fact]
public void EditMovie_ThatExists_ShouldUpdateMovieDetails()
{
    // Arrange
    var m1 = svc.createNewMovie(new Movie
    {
        Title = "Inception",
        Year = 2010,
        MovieGenre = Movie.Genre.SciFi,
        PhotoURL = "https://i.ibb.co/LgKCDJh/Inception.jpg"
    });

    // Act
    var movieEdit = svc.editMovie(
        new Movie
        {
            MovieId = m1.MovieId,
            Title = "Inception Updated",
            MovieGenre = Movie.Genre.Action,
            PhotoURL = "https://i.ibb.co/5Bh8yzM/Forrest.jpg"
        });
    
    var updatedMovie = svc.getMovieById(m1.MovieId);

    // Assert
    Assert.NotNull(updatedMovie);
    Assert.Equal(movieEdit.Title, updatedMovie.Title);
    Assert.Equal(movieEdit.MovieGenre, updatedMovie.MovieGenre);
    Assert.Equal(movieEdit.PhotoURL, updatedMovie.PhotoURL);
}

[Fact]
public void EditMovie_WithNonExistingMovie_ShouldReturnNull()
{
    // Arrange
    var nonExistingMovie = new Movie
    {
        MovieId = 999,
        Title = "Non-existing Movie",
        Year = 2020,
        MovieGenre = Movie.Genre.Action,
        PhotoURL = "https://example.com/non-existing.jpg"
    };

    // Act
    var updatedMovie = svc.editMovie(nonExistingMovie);

    // Assert
    Assert.Null(updatedMovie);
}


[Fact]
public void DeleteMovieById_ThatExists_ShouldRemoveMovie()
{
    // Arrange
    var movie = svc.createNewMovie(new Movie
    {
        Title = "The Dark Knight",
        Year = 2008,
        MovieGenre = Movie.Genre.Action,
        PhotoURL = "https://i.ibb.co/4ZjS1KJ/Dark-Knight-Movie-Poster.jpg"
    });

    // Act
    var isDeleted = svc.deleteMovieById(movie.MovieId);

    // Assert
    Assert.True(isDeleted);
    var deletedMovie = svc.getMovieById(movie.MovieId);
    Assert.Null(deletedMovie);
}

[Fact]
public void DeleteMovieById_WithNonExistingId_ShouldReturnFalse()
{
    // Arrange
    var nonExistingId = 999;

    // Act
    var isDeleted = svc.deleteMovieById(nonExistingId);

    // Assert
    Assert.False(isDeleted);
}

[Fact]
public void GetHighestRankingMovies_ShouldReturnTop5MoviesByRating()
{
    // Arrange

    //add movies
    var m1 = svc.createNewMovie (new Movie {
            Title = "The Shawshank Redemption", Year = 1994, MovieGenre = Movie.Genre.Drama, PhotoURL = "https://i.ibb.co/KxmqKj9/Shawshank-Redemption-Movie-Poster.jpg"});
    var m2 = svc.createNewMovie(new Movie{
            Title = "The Godfather",Year = 1972,MovieGenre = Movie.Genre.Crime,PhotoURL = "https://i.ibb.co/GCSPkTT/The-Godfather.jpg"});
    var m3 = svc.createNewMovie(new Movie{
            Title = "The Dark Knight",Year = 2008,MovieGenre = Movie.Genre.Action,PhotoURL = "https://i.ibb.co/wzrrDjW/The-Dark-Knight.jpg"});
    var m4 = svc.createNewMovie(new Movie{
            Title = "Inception",Year = 2010,MovieGenre = Movie.Genre.SciFi,PhotoURL = "https://i.ibb.co/LgKCDJh/Inception.jpg"});
    var m5 = svc.createNewMovie(new Movie{
            Title = "Forrest Gump",Year = 1994,MovieGenre = Movie.Genre.Drama,PhotoURL = "https://i.ibb.co/5Bh8yzM/Forrest.jpg"});
    var m6 = svc.createNewMovie(new Movie{
            Title = "The Matrix",Year = 1999,MovieGenre = Movie.Genre.Action,PhotoURL = "https://i.ibb.co/Jct25RN/The-Matrix.jpg"});
    var m7 = svc.createNewMovie(new Movie{
            Title = "Pulp Fiction",Year = 1994,MovieGenre = Movie.Genre.Crime,PhotoURL = "https://i.ibb.co/C81nZZt/PulpF.jpg"});

    //add reviews

    var r2 = svc.addReviewOnCurrentMovie(new Review{
            MovieId = m1.MovieId, ReviewAuthor = "Bob", Rating = 5});
    var r3 = svc.addReviewOnCurrentMovie(new Review{
            MovieId = m2.MovieId, ReviewAuthor = "Charlie", Rating = 4});
    var r4 = svc.addReviewOnCurrentMovie(new Review{
            MovieId = m3.MovieId, ReviewAuthor = "Diana", Rating = 4});
    var r5 = svc.addReviewOnCurrentMovie(new Review{
            MovieId = m4.MovieId, ReviewAuthor = "Eva", Rating = 5});
    var r6 = svc.addReviewOnCurrentMovie(new Review{
            MovieId = m5.MovieId, ReviewAuthor = "Frank", Rating = 5});
    var r7 = svc.addReviewOnCurrentMovie(new Review{
            MovieId = m6.MovieId, ReviewAuthor = "Grace", Rating = 1});
    var r8 = svc.addReviewOnCurrentMovie(new Review{
            MovieId = m6.MovieId, ReviewAuthor = "Henry", Rating = 3});

    // Act
    var topMovies = svc.GetHighestRankingMovies();

    // Assert
    Assert.NotNull(topMovies);
    Assert.Equal(5, topMovies.Count);
    Assert.Contains(topMovies, m => m.Title == "The Shawshank Redemption");
    Assert.Contains(topMovies, m => m.Title == "The Godfather");
    Assert.Contains(topMovies, m => m.Title == "The Dark Knight");
    Assert.Contains(topMovies, m => m.Title == "Inception");
    Assert.Contains(topMovies, m => m.Title == "Forrest Gump");
    Assert.DoesNotContain(topMovies, m => m.Title == "The Matrix");
    Assert.DoesNotContain(topMovies, m => m.Title == "Pulp Fiction");

}

[Fact]
public void SearchMovies_WithMatchingQuery_ShouldReturnMatchingMovies()
{
    // Arrange
    var movie1 = svc.createNewMovie(new Movie
    {
        Title = "The Matrix",
        Year = 1999,
        MovieGenre = Movie.Genre.SciFi,
    });

    var movie2 = svc.createNewMovie(new Movie
    {
        Title = "Inception",
        Year = 2010,
        MovieGenre = Movie.Genre.SciFi,
    });

    // Act
    var result = svc.SearchMovies("Matrix");

    // Assert
    Assert.NotNull(result);
    Assert.NotEmpty(result);
    Assert.Contains(result, m => m.Title == "The Matrix");
    Assert.DoesNotContain(result, m => m.Title == "Inception");
}

[Fact]
public void SearchMovies_WithNonMatchingQuery_ShouldReturnEmptyList()
{
    // Arrange
    svc.createNewMovie(new Movie
    {
        Title = "The Matrix",
        Year = 1999,
        MovieGenre = Movie.Genre.SciFi,
    });

    svc.createNewMovie(new Movie
    {
        Title = "Inception",
        Year = 2010,
        MovieGenre = Movie.Genre.SciFi,
    });

    // Act
    var result = svc.SearchMovies("Testing No Movie Match");

    // Assert
    Assert.NotNull(result);
    Assert.Empty(result);
}

// ========================== Movie Review Management Tests  =========================

[Fact]
public void AddReviewOnCurrentMovie_WithValidReview_ShouldReturnNewReview()
{
    // Arrange
    var m1 = svc.createNewMovie (new Movie {
            Title = "The Shawshank Redemption", Year = 1994, MovieGenre = Movie.Genre.Drama, PhotoURL = "https://i.ibb.co/KxmqKj9/Shawshank-Redemption-Movie-Poster.jpg"});
    var r2 = svc.addReviewOnCurrentMovie(new Review{
            MovieId = m1.MovieId, ReviewAuthor = "Bob", Rating = 5, ReviewText = "Test Review" });
    
    // Act
    var result = svc.GetReviewById(r2.Id);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(r2.ReviewAuthor, result.ReviewAuthor);
    Assert.Equal(r2.Rating, result.Rating);
    Assert.Equal(r2.ReviewText, result.ReviewText);
}

[Fact]
public void EditReview_WithValidReview_ShouldUpdateReview()
{
    // Arrange
    var m1 = svc.createNewMovie (new Movie {
            Title = "The Shawshank Redemption", Year = 1994, MovieGenre = Movie.Genre.Drama, PhotoURL = "https://i.ibb.co/KxmqKj9/Shawshank-Redemption-Movie-Poster.jpg"});
    var r2 = svc.addReviewOnCurrentMovie(new Review{
            MovieId = m1.MovieId, ReviewAuthor = "Bob", Rating = 5, ReviewText = "Test Review" });

    // Act
    var updateReview = svc.EditReview(new Review{
            MovieId = m1.MovieId, Id = r2.Id, ReviewAuthor = "BobUpdated", Rating = 1, ReviewText = "Updated Review" });

    // Assert
    Assert.NotNull(updateReview);
    Assert.Equal("BobUpdated", updateReview.ReviewAuthor);
    Assert.Equal(1, updateReview.Rating);
    Assert.Equal("Updated Review", updateReview.ReviewText);
}

[Fact]
public void EditReview_WithNonExistingReviewId_ShouldReturnNull()
{
    // Arrange
    var m1 = svc.createNewMovie (new Movie {
            Title = "The Shawshank Redemption", Year = 1994, MovieGenre = Movie.Genre.Drama, PhotoURL = "https://i.ibb.co/KxmqKj9/Shawshank-Redemption-Movie-Poster.jpg"});
    var r2 = svc.addReviewOnCurrentMovie(new Review{
            MovieId = m1.MovieId, ReviewAuthor = "Bob", Rating = 5, ReviewText = "Test Review" });

    var nonExistingReview = new Review
    {
        Id = 999,
        MovieId = m1.MovieId,
        ReviewAuthor = "Non-existing Author",
        Rating = 3,
        ReviewText = "Non-existing Review"
    };

    // Act
    var updatedReview = svc.EditReview(nonExistingReview);
    var unchangedReview = svc.GetReviewById(r2.Id);

    // Assert
    Assert.Null(updatedReview);
    Assert.NotEqual(r2.Id, nonExistingReview.Id);
    Assert.Equal(r2.Id, unchangedReview.Id);
    Assert.Equal(r2.ReviewAuthor, unchangedReview.ReviewAuthor);
    Assert.Equal(r2.Rating, unchangedReview.Rating);
    Assert.Equal(r2.ReviewText, unchangedReview.ReviewText);
}

[Fact]
public void EditReview_ChangeMovie_ShouldNotChangeMovie()
{
    // Arrange
    var m1 = svc.createNewMovie (new Movie {
            Title = "The Shawshank Redemption", Year = 1994, MovieGenre = Movie.Genre.Drama, PhotoURL = "https://i.ibb.co/KxmqKj9/Shawshank-Redemption-Movie-Poster.jpg"});
    var r2 = svc.addReviewOnCurrentMovie(new Review{
            MovieId = m1.MovieId, ReviewAuthor = "Bob", Rating = 5, ReviewText = "Test Review" });

    // Act
    var updateReview = svc.EditReview(new Review{
            MovieId = 999, Id = r2.Id, ReviewAuthor = "BobUpdated", Rating = 1, ReviewText = "Updated Review" });
    var changedReview = svc.GetReviewById(r2.Id);

    // Assert
    Assert.NotNull(updateReview);
    Assert.Equal(updateReview.Id, changedReview.Id);
    Assert.Equal(updateReview.ReviewAuthor, changedReview.ReviewAuthor);
    Assert.Equal(updateReview.Rating, changedReview.Rating);
    Assert.Equal(updateReview.ReviewText, changedReview.ReviewText);
    Assert.Equal(m1.MovieId, changedReview.MovieId);
}

    
[Fact]
public void GetReviewById_WithNonExistingId_ShouldReturnNull()
{
    // Arrange
    var m1 = svc.createNewMovie (new Movie {
            Title = "The Shawshank Redemption", Year = 1994, MovieGenre = Movie.Genre.Drama, PhotoURL = "https://i.ibb.co/KxmqKj9/Shawshank-Redemption-Movie-Poster.jpg"});
    svc.addReviewOnCurrentMovie(new Review{
            MovieId = m1.MovieId, ReviewAuthor = "Bob", Rating = 5, ReviewText = "Test Review" });
    
    // Act
    var result = svc.GetReviewById(999);

    // Assert
    Assert.Null(result);
}

}











// ==================== UserService Tests =============================
[Collection("Sequential")]
public class UserServiceTests
{
    private readonly IUserService svc;

    public UserServiceTests()
    {
        // general arrangement
        svc = new UserServiceDb();
        
        // ensure data source is empty before each test
        svc.Initialise();
    }

    // ========================== User Tests =========================

    [Fact] // --- Register Valid User test
    public void User_Register_WhenValid_ShouldReturnUser()
    {
        // arrange 
        var reg = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);
        
        // act
        var user = svc.GetUserByEmail(reg.Email);
        
        // assert
        Assert.NotNull(reg);
        Assert.NotNull(user);
    } 

    [Fact] // --- Register Duplicate Test
    public void User_Register_WhenDuplicateEmail_ShouldReturnNull()
    {
        // arrange 
        var s1 = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);
        
        // act
        var s2 = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);

        // assert
        Assert.NotNull(s1);
        Assert.Null(s2);
    } 

    [Fact] // --- Authenticate Invalid Test
    public void User_Authenticate_WhenInValidCredentials_ShouldReturnNull()
    {
        // arrange 
        var s1 = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);
    
        // act
        var user = svc.Authenticate("xxx@email.com", "guest");
        // assert
        Assert.Null(user);

    } 

    [Fact] // --- Authenticate Valid Test
    public void User_Authenticate_WhenValidCredentials_ShouldReturnUser()
    {
        // arrange 
        var s1 = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);
    
        // act
        var user = svc.Authenticate("xxx@email.com", "admin");
        
        // assert
        Assert.NotNull(user);
    } 
 
}


