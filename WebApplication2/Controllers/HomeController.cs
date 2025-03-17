using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using WebApplication2.Models; // Session için gerekli

public class HomeController : Controller
{
    public IActionResult Index()
    {
        // Kullanýcý bilgilerini Cookie'den al
        var userInfo = Request.Cookies["UserInfo"];

        
        ViewBag.IsLoggedIn = !string.IsNullOrEmpty(userInfo);
        ViewBag.UserName = userInfo;

        // Film bilgilerini Session'a kaydediyoruz sadece bir kez
        if (HttpContext.Session.GetString("Movies") == null)
        {
            // Film verilerini kaydetme iþlemi
            List<Movie> movies = new List<Movie>
        {
            new Movie { MovieID = 1, Title = "X-Men: The Last Stand", Director = "Brett Ratner", Stars = new string[] { "Patrick Stewart", "Hugh Jackman", "Halle Berry" }, ReleaseYear = 2006, ImageUrl = "xmen.jpg" },
            new Movie { MovieID = 2, Title = "Spider Man 2", Director = "Sam Raimi", Stars = new string[] { "Tobey Maguire", "Kirsten Dunst", "Alfred Molina" }, ReleaseYear = 2004, ImageUrl = "spiderman2.jpg" },
            new Movie { MovieID = 3, Title = "Spider Man 3", Director = "Sam Raimi", Stars = new string[] { "Tobey Maguire", "Kirsten Dunst", "Topher Grace" }, ReleaseYear = 2007, ImageUrl = "spiderman3.jpg" },
            new Movie { MovieID = 4, Title = "Valkyrie", Director = "Bryan Singer", Stars = new string[] { "Tom Cruise", "Bill Nighy", "Carice van Houten" }, ReleaseYear = 2008, ImageUrl = "valkyrie.jpg" },
            new Movie { MovieID = 5, Title = "Gladiator", Director = "Ridley Scott", Stars = new string[] { "Russell Crowe", "Joaquin Phoenix", "Connie Nielsen" }, ReleaseYear = 2000, ImageUrl = "gladiator.jpg" }
        };

            HttpContext.Session.SetString("Movies", JsonConvert.SerializeObject(movies));
        }

        // Film listesini çekiyoruz
        var movieList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("Movies"));

        return View(movieList);  // Ana sayfada film listesi gösterilecek
    }

    public IActionResult Logout()
    {
        // Session’ý temizle
        HttpContext.Session.Clear();

        // Cookie’yi temizle
        Response.Cookies.Delete("UserInfo");

        // Login sayfasýna yönlendir
        return RedirectToAction("Index", "Account");
    }

}
