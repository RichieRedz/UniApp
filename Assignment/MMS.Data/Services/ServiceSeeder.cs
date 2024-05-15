using MMS.Data.Entities;
namespace MMS.Data.Services;

public static class ServiceSeeder
{

    // default seeder using Db versions of services
    public static void Seed()
    {
        IUserService usvc = new UserServiceDb();
        IMovieService rsvc = new MovieServiceDb();
       
        usvc.Initialise();

        SeedUsers(usvc);
        SeedMovies(rsvc);
    }

    // use this method FIRST to seed the database with dummy test data using an IUserService
    private static void SeedUsers(IUserService usvc)
    {
        // Note: do not call initialise here

            usvc.Register("admin","admin@mail.com","password",Role.admin);
       
    }
    
    // use this method SECOND to seed the database with dummy test data using an IRecipeService
    private static void SeedMovies(IMovieService rsvc)
    {        
        // Note: do not call initialise here

        // add relevant movie seed data

            
            var m1 = rsvc.createNewMovie (new Movie {
                Title = "The Shawshank Redemption",
                Year = 1994,
                MovieGenre = Movie.Genre.Drama,
                PhotoURL = "https://i.ibb.co/KxmqKj9/Shawshank-Redemption-Movie-Poster.jpg"
            });
            var m2 = rsvc.createNewMovie(new Movie{
                Title = "The Godfather",
                Year = 1972,
                MovieGenre = Movie.Genre.Crime,
                PhotoURL = "https://i.ibb.co/GCSPkTT/The-Godfather.jpg"
            });
            var m3 = rsvc.createNewMovie(new Movie{
                Title = "The Dark Knight",
                Year = 2008,
                MovieGenre = Movie.Genre.Action,
                PhotoURL = "https://i.ibb.co/wzrrDjW/The-Dark-Knight.jpg"
            });
            var m4 = rsvc.createNewMovie(new Movie{
                Title = "Inception",
                Year = 2010,
                MovieGenre = Movie.Genre.SciFi,
                PhotoURL = "https://i.ibb.co/LgKCDJh/Inception.jpg"
            });
            var m5 = rsvc.createNewMovie(new Movie{
                Title = "Forrest Gump",
                Year = 1994,
                MovieGenre = Movie.Genre.Drama,
                PhotoURL = "https://i.ibb.co/5Bh8yzM/Forrest.jpg"
            });
            var m6 = rsvc.createNewMovie(new Movie
            {
                Title = "The Matrix",
                Year = 1999,
                MovieGenre = Movie.Genre.Action,
                PhotoURL = "https://i.ibb.co/Jct25RN/The-Matrix.jpg"
            });
            var m7 = rsvc.createNewMovie(new Movie
            {
                Title = "Pulp Fiction",
                Year = 1994,
                MovieGenre = Movie.Genre.Crime,
                PhotoURL = "https://i.ibb.co/C81nZZt/PulpF.jpg"
            });
            var m8 = rsvc.createNewMovie(new Movie
            {
                Title = "Interstellar",
                Year = 2014,
                MovieGenre = Movie.Genre.SciFi,
                PhotoURL = "https://i.ibb.co/0Vp7S9p/Interstellar.jpg"
            });
            var m9 = rsvc.createNewMovie(new Movie
            {
                Title = "The Silence of the Lambs",
                Year = 1991,
                MovieGenre = Movie.Genre.Thriller,
                PhotoURL = "https://i.ibb.co/dBK5KRs/Silence-of-the-lambs.jpg"
            });
            var m10 = rsvc.createNewMovie(new Movie
            {
                Title = "The Lord of the Rings: The Fellowship of the Ring",
                Year = 2001,
                MovieGenre = Movie.Genre.Fantasy,
                PhotoURL = "https://i.ibb.co/BByRMJy/LOTR.jpg"
            });
            var m11 = rsvc.createNewMovie(new Movie
            {
                Title = "The Avengers",
                Year = 2012,
                MovieGenre = Movie.Genre.Action,
                PhotoURL = "https://i.ibb.co/dpJJk77/Avengers.jpg"
            });
            var m12 = rsvc.createNewMovie(new Movie
            {
                Title = "Titanic",
                Year = 1997,
                MovieGenre = Movie.Genre.Romance,
                PhotoURL = "https://i.ibb.co/ZW4m90f/Titanic.jpg"
            });
            var m13 = rsvc.createNewMovie(new Movie
            {
                Title = "Jurassic Park",
                Year = 1993,
                MovieGenre = Movie.Genre.Adventure,
                PhotoURL = "https://i.ibb.co/8NnXjQJ/Jurassic-Park.jpg"
            });

            var r1 = rsvc.addReviewOnCurrentMovie(new Review
            {
                MovieId = m1.MovieId,
                ReviewAuthor = "Alice", 
                Rating = 4 ,
                ReviewText = "Fantastic movie! The plot was engaging and the characters were well-developed. Highly recommended."
            });
            var r2 = rsvc.addReviewOnCurrentMovie(new Review
            {
                MovieId = m1.MovieId, ReviewAuthor = "Bob", Rating = 5, ReviewText = "One of the best movies I've ever seen. The cinematography was breathtaking and the story kept me on the edge of my seat."
            });
            var r3 = rsvc.addReviewOnCurrentMovie(new Review
            {
                MovieId = m2.MovieId, ReviewAuthor = "Charlie", Rating = 3, ReviewText = "Decent movie, but I expected more from the storyline. The acting was good, though."
            });
            var r4 = rsvc.addReviewOnCurrentMovie(new Review
            {
                MovieId = m2.MovieId, ReviewAuthor = "Diana", Rating = 4, ReviewText = "Enjoyed watching this film. The visuals were stunning and the soundtrack was amazing."
            });
            var r5 = rsvc.addReviewOnCurrentMovie(new Review
            {
                MovieId = m4.MovieId, ReviewAuthor = "Eva", Rating = 4, ReviewText = "This movie exceeded my expectations. It was both thrilling and emotional."
            });
            var r6 = rsvc.addReviewOnCurrentMovie(new Review
            {
                MovieId = m6.MovieId, ReviewAuthor = "Frank", Rating = 2, ReviewText = "Disappointing film. The plot was confusing and the pacing was off."
            });
            var r7 = rsvc.addReviewOnCurrentMovie(new Review
            {
                MovieId = m8.MovieId, ReviewAuthor = "Grace", Rating = 5, ReviewText = "Absolutely loved it! A must-watch for all movie enthusiasts."
            });
            var r8 = rsvc.addReviewOnCurrentMovie(new Review
            {
                MovieId = m10.MovieId, ReviewAuthor = "Henry", Rating = 3, ReviewText = "Average movie. It had its moments but overall didn't leave a lasting impression."
            });
            var r9 = rsvc.addReviewOnCurrentMovie(new Review
            {
                MovieId = m11.MovieId, ReviewAuthor = "Isabel", Rating = 5, ReviewText = "Brilliant storytelling and amazing performances. A masterpiece."
            });
            var r10 = rsvc.addReviewOnCurrentMovie(new Review
            {
                MovieId = m13.MovieId, ReviewAuthor = "Jack",  Rating = 4 , ReviewText = "Great movie with a powerful message. Left me thinking long after it ended."
            });


    }

}


