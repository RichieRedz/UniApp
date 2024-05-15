using System.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMS.Data.Entities;
using MMS.Data.Services;
using MMS.Web.Models;

namespace MMS.Web.Controllers;

public class MovieController : BaseController
{
    private IMovieService rsvc;

    public MovieController(IMovieService _rsvc)
    {
        rsvc = _rsvc;            
    }
    // GET: Movie
    public IActionResult Index()
    {
        var movies = rsvc.displayMovieCollection();
        var viewModel = new MovieSearchViewModel { Movies = movies };
        return View(viewModel);
    }
    
    // GET: Movie/index
    [HttpGet("Movie/Search")]
    public IActionResult Index(MovieSearchViewModel search)
    {                  
        // set the viewmodel Tickets property by calling service method 
        // using the range and query values from the viewmodel 
        search.Movies = rsvc.SearchMovies(query: search.Query);
         
        return View(search);
    } 

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // [Authorize(Roles="admin")]

    // GET: Movie/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Movie/Create
    [HttpPost]
    public IActionResult Create(MovieReviewViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            // Call a service method to add the movie to the data store
            var addedMovie = rsvc.createNewMovie(viewModel.Movie);
            viewModel.Review.MovieId = addedMovie.MovieId;
            rsvc.addReviewOnCurrentMovie(viewModel.Review);

            if (addedMovie != null)
            {
                // Redirect to the details page of the newly created movie
                return RedirectToAction("Details", new { id = addedMovie.MovieId });
            }
            else
            {
                // Handle if movie couldn't be added (e.g., database error)
                return RedirectToAction("Error");
            }
        }
        // If model state is not valid, redisplay the form with validation errors
        return View(viewModel);
    }

    // GET: Movie/Details/{id}
    public IActionResult Details(int id)
    {
        var movie = rsvc.getMovieById(id);

        if(movie is null)
        {
            Alert("Movie Not Found.", AlertType.warning);
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: Movie/Edit/{id}
    public IActionResult Edit(int id)
    {
        var movie = rsvc.getMovieById(id);

        if(movie == null)
        {
            Alert("Movie Not Found.", AlertType.warning);
            return RedirectToAction(nameof(Index));
        }

        return View(movie);
    }

    // POST: Movie/Edit/{id}
    [HttpPost]
    public IActionResult Edit(Movie m)
    {
        if(ModelState.IsValid)
        {
            rsvc.editMovie(m);
            return RedirectToAction(nameof(Details), new { id = m.MovieId });
        }

        return View(m);
    }

    // GET: Movie/Delete/{id}
    public IActionResult Delete(int id)
    {
        var movie = rsvc.getMovieById(id);

        if(movie == null)
        {
            Alert("Movie Not Found.", AlertType.warning);
            return RedirectToAction(nameof(Index));
        }

        return View(movie);
    }

    // POST: Movie/Delete/{id}
    public IActionResult DeleteConfirmed(int movieId)
    {
        // delete Movie
        var deleted = rsvc.deleteMovieById(movieId);
        if (deleted)
        {
            Alert("Movie Deleted", AlertType.success);
        }
        else
        {
            Alert("Movie couldn't be deleted", AlertType.warning);
        }
        
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Reviews()
    {
        var allReviews = rsvc.getReviewList();

        var viewModel = new MovieReviewViewModel
        {
            AllReviews = allReviews.Select(review => new Review
            {
                Id = review.Id,
                ReviewAuthor = review.ReviewAuthor,
                Rating = review.Rating,
                ReviewText = review.ReviewText,
                MovieId = review.MovieId,
                Date = review.Date

            }).ToList()
        };
    return View(viewModel);
    }

    // GET: Movie/NewReview
    public IActionResult NewReview(int id)
    {
        var movie = rsvc.getMovieById(id);
        ViewBag.MovieTitle = movie.Title;

        var viewModel = new Review
        {
            MovieId = movie.MovieId
        };
        
        return View(viewModel);
    }

    // POST: Movie/NewReview/{Review}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult NewReview(Review r)
    {
        if (ModelState.IsValid)
        {
            rsvc.addReviewOnCurrentMovie(r);
            return RedirectToAction(nameof(Details), new { id = r.MovieId });
        }
        return View(r);
    }

    // Get: Movie/EditReview/{id}

    public IActionResult EditReview(int Id)
    {
        var review = rsvc.GetReviewById(Id);

        if(review == null)
        {
            Alert("Review Not Found.", AlertType.warning);
            return RedirectToAction(nameof(Details), new { id = review.MovieId });
        }

        return View(review);
    }

    [HttpPost]
    public IActionResult EditReview(Review r)
    {
        if (ModelState.IsValid)
        {
            rsvc.EditReview(r);
            return RedirectToAction(nameof(Details), new { id = r.MovieId });
        }
        return View(r);
    }

}

