using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string firstName, string lastName)
    {
        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
        {
            string fullName = firstName + " " + lastName;

            // ✅ Store user info in Session (Ensures session works)
            HttpContext.Session.SetString("UserInfo", fullName);

            // ✅ Store user info in Cookies (Valid for 1 month)
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddMonths(1),
                HttpOnly = true // Optional: Improves security by preventing JS access
            };
            Response.Cookies.Append("UserInfo", fullName, options);

            // ✅ Redirect to Home Page
            return RedirectToAction("Index", "Home");
        }

        // ❌ Show error if input is missing
        ViewBag.Error = "Lütfen adınızı ve soyadınızı giriniz!";
        return View("Index");
    }

    public IActionResult Logout()
    {

        // ✅ Clear Session (Prevents logged-in state)
        HttpContext.Session.Remove("UserInfo");
        HttpContext.Session.Remove("Cart");

        // ✅ Delete Cookie (Ensures user is logged out)
        Response.Cookies.Delete("UserInfo");

        // ✅ Redirect to Home Page
        return RedirectToAction("Index", "Home");
    }
}
