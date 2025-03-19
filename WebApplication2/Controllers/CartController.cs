using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WebApplication2.Models;

public class CartController : Controller
{
    public IActionResult Cart()
    {
       
        var userCookie = Request.Cookies["UserInfo"];
        if (string.IsNullOrEmpty(userCookie))
        {
            TempData["ErrorMessage"] = "You must log in to access the cart.";
            return RedirectToAction("Index", "Account");
        }

        var movieListJson = HttpContext.Session.GetString("Movies");
        var cartJson = HttpContext.Session.GetString("Cart");

        if (string.IsNullOrEmpty(movieListJson) || string.IsNullOrEmpty(cartJson))
        {
            return RedirectToAction("CartEmpty");
        }

        var movies = JsonSerializer.Deserialize<List<Movie>>(movieListJson);
        var cartMovieIds = JsonSerializer.Deserialize<List<int>>(cartJson);
        var cartMovies = movies.Where(m => cartMovieIds.Contains(m.MovieID)).ToList();

        if (cartMovies.Count == 0)
        {
            return RedirectToAction("CartEmpty");
        }

        return View(cartMovies);
    }

    public IActionResult CartEmpty()
    {
        return View();
    }

    
    [HttpPost]
    public IActionResult RemoveFromCart(int movieId)
    {
        var cartJson = HttpContext.Session.GetString("Cart");
        if (string.IsNullOrEmpty(cartJson))
        {
            return Json(new { success = false, message = "Cart is empty." });
        }

        var cartMovieIds = JsonSerializer.Deserialize<List<int>>(cartJson);
        if (cartMovieIds.Contains(movieId))
        {
            cartMovieIds.Remove(movieId);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartMovieIds));
        }

        return Json(new { success = true, message = "Movie removed from cart." });
    }
}
