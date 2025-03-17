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
        // Check if the user is logged in
        var userCookie = Request.Cookies["UserInfo"];
        if (string.IsNullOrEmpty(userCookie))
        {
            // Redirect to login page if not logged in
            TempData["ErrorMessage"] = "You must log in to access the cart.";
            return RedirectToAction("Index", "Account");
        }

        var movieListJson = HttpContext.Session.GetString("Movies");
        var cartJson = HttpContext.Session.GetString("Cart");

        if (movieListJson == null || cartJson == null)
        {
            ViewBag.Message = "Your cart is empty.";
            return View("CartEmpty");
        }

        var movies = JsonSerializer.Deserialize<List<Movie>>(movieListJson);
        var cartMovieIds = JsonSerializer.Deserialize<List<int>>(cartJson);
        var cartMovies = movies.Where(m => cartMovieIds.Contains(m.MovieID)).ToList();

        if (cartMovies.Count == 0)
        {
            ViewBag.Message = "Your cart is empty.";
            return View("CartEmpty");
        }

        return View(cartMovies);
    }
}
