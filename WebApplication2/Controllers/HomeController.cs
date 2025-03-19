using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using WebApplication2.Models;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        // Check if the user is logged in by reading from Session
        var userInfo = HttpContext.Session.GetString("UserInfo");

        if (!string.IsNullOrEmpty(userInfo))
        {
            ViewBag.IsLoggedIn = true;
            ViewBag.UserName = userInfo;
        }
        else
        {
            ViewBag.IsLoggedIn = false;
            ViewBag.UserName = "Guest";
        }

        // Store movies in session only once
        if (HttpContext.Session.GetString("Movies") == null)
        {
            List<Movie> movies = new List<Movie>
            {
                   new Movie { MovieID = 1, Title = "X-Men: The Last Stand", Director = "Brett Ratner", Stars = new string[] { "Patrick Stewart", "Hugh Jackman", "Halle Berry" }, ReleaseYear = 2006, ImageUrl = "https://images.moviesanywhere.com/baf6d2e1328f135627d528f4109b7df1/5ce38f9d-3b89-41e7-9793-c77f0d8711b3.webp?h=375&resize=fit&w=250" },
new Movie { MovieID = 2, Title = "Spider Man 2", Director = "Sam Raimi", Stars = new string[] { "Tobey Maguire", "Kirsten Dunst", "Alfred Molina" }, ReleaseYear = 2004, ImageUrl = "https://images.moviesanywhere.com/e45bfc010f1e0626b1ee9efbe2726e55/7e42ca11-be74-41b9-986c-3e5a8a431fe3.webp?h=375&resize=fit&w=250" },
new Movie { MovieID = 3, Title = "Spider Man 3", Director = "Sam Raimi", Stars = new string[] { "Tobey Maguire", "Kirsten Dunst", "Topher Grace" }, ReleaseYear = 2007, ImageUrl = "https://images.moviesanywhere.com/e6699aa9e55cfbe140035ef66f934c05/9aeb55f2-b6b9-44e8-9b43-b0fada44de7b.webp?h=375&resize=fit&w=250" },
new Movie { MovieID = 4, Title = "Valkyrie", Director = "Bryan Singer", Stars = new string[] { "Tom Cruise", "Bill Nighy", "Carice van Houten" }, ReleaseYear = 2008, ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/b8/Valkyrie_poster.jpg" },
new Movie { MovieID = 5, Title = "Gladiator", Director = "Ridley Scott", Stars = new string[] { "Russell Crowe", "Joaquin Phoenix", "Connie Nielsen" }, ReleaseYear = 2000, ImageUrl = "https://upload.wikimedia.org/wikipedia/en/f/fb/Gladiator_%282000_film_poster%29.png" },
new Movie { MovieID = 6, Title = "Fight Club", Director = "David Fincher", Stars = new string[] { "Brad Pitt", "Edward Norton", "Helena Bonham" }, ReleaseYear = 1999, ImageUrl = "https://upload.wikimedia.org/wikipedia/en/f/fc/Fight_Club_poster.jpg" },
new Movie { MovieID = 7, Title = "Valkyrie", Director = "Bryan Singer", Stars = new string[] { "Tom Cruise", "Bill Nighy", "Carice van Houten" }, ReleaseYear = 2008, ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/b8/Valkyrie_poster.jpg" },
new Movie { MovieID = 8, Title = "Gladiator", Director = "Ridley Scott", Stars = new string[] { "Russell Crowe", "Joaquin Phoenix", "Connie Nielsen" }, ReleaseYear = 2000, ImageUrl = "https://upload.wikimedia.org/wikipedia/en/f/fb/Gladiator_%282000_film_poster%29.png" },
new Movie { MovieID = 9, Title = "Fight Club", Director = "David Fincher", Stars = new string[] { "Brad Pitt", "Edward Norton", "Helena Bonham" }, ReleaseYear = 1999, ImageUrl = "https://upload.wikimedia.org/wikipedia/en/f/fc/Fight_Club_poster.jpg" }
        };

            HttpContext.Session.SetString("Movies", JsonConvert.SerializeObject(movies));
        }

        // Retrieve movie list
        var movieList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("Movies"));

        return View(movieList);
    }
}
