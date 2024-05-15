using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MMS.Web.Models;
using MMS.Data.Services;

namespace MMS.Web.Controllers;

public class HomeController : Controller
{
    private readonly IMovieService rsvc;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger,IMovieService _rsvc)
    {
        _logger = logger;
        rsvc = _rsvc;
    }

    public IActionResult Index()
    {
        var topRatedMovies = rsvc.GetHighestRankingMovies();

        var viewModel = new TopMovieViewModel
        {
            TopRatedMovies = topRatedMovies.Select(movie => new TopMovieViewModel
            {
                Title = movie.Title,
                PhotoURL = movie.PhotoURL,
                Rating = movie.AvgReview,
                MovieId = movie.MovieId
            }).ToList()
        };

    return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult ContactUs()
    {
        return View();
    }
}
