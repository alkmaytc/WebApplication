using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WebApplication2.Models;

public class MoviesController : Controller
{
    public IActionResult MovieInfo(int? id)
    {
        // Retrieve movie list from session
        var movieListJson = HttpContext.Session.GetString("Movies");
        if (movieListJson == null)
        {
            return Content("No movies available.");
        }

        var movies = JsonSerializer.Deserialize<List<Movie>>(movieListJson);

        // Validate movie ID
        if (!id.HasValue)
        {
            ViewBag.Message = "Movie ID is missing.";
            return View("MovieInfoError");
        }

        var movie = movies.FirstOrDefault(m => m.MovieID == id.Value);
        if (movie == null)
        {
            ViewBag.Message = "Invalid Movie ID.";
            return View("MovieInfoError");
        }

        return View(movie);
    }

    [HttpPost]
    public IActionResult AddToCart(int id)
    {
        List<int> cart;
        var cartJson = HttpContext.Session.GetString("Cart");

        if (cartJson == null)
        {
            cart = new List<int>();
        }
        else
        {
            cart = JsonSerializer.Deserialize<List<int>>(cartJson);
        }

        bool alreadyAdded = cart.Contains(id);

        if (!alreadyAdded)
        {
            cart.Add(id);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
        }

        return Json(new { success = true, alreadyAdded = alreadyAdded });
    }
    public IActionResult AddToCartInfo(int id)
    {
        var cartJson = HttpContext.Session.GetString("Cart");
        List<int> cart = string.IsNullOrEmpty(cartJson) ? new List<int>() : JsonSerializer.Deserialize<List<int>>(cartJson);

        if (!cart.Contains(id))
        {
            cart.Add(id);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
            TempData["Message"] = "The movie is added for the first time!";
        }
        else
        {
            TempData["Message"] = "The movie is already in the list!";
        }

        return RedirectToAction("MovieInfo", new { id });
    }
}
