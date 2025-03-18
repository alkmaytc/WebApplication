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
        // Kullanıcının giriş yapıp yapmadığını kontrol et
        var userCookie = Request.Cookies["UserInfo"];
        if (string.IsNullOrEmpty(userCookie))
        {
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

    // 🛒 Sepetten film çıkarma işlemi
    
   /* public IActionResult RemoveFromCart(int id)
    {
        var cartJson = HttpContext.Session.GetString("Cart");
        if (cartJson == null)
        {
            return Json(new { success = false, message = "Cart is empty." });
        }

        var cartMovieIds = JsonSerializer.Deserialize<List<int>>(cartJson);
        if (cartMovieIds.Contains(id))
        {
            cartMovieIds.Remove(id);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartMovieIds));
        }
        return View(cartMovies);

    }
*/
}
