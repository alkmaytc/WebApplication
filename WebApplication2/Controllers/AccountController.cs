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

            
            HttpContext.Session.SetString("UserInfo", fullName);

           
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddMonths(1),
                HttpOnly = true 
            };
            Response.Cookies.Append("UserInfo", fullName, options);

           
            return RedirectToAction("Index", "Home");
        }

        
        ViewBag.Error = "Lütfen adınızı ve soyadınızı giriniz!";
        return View("Index");
    }

    public IActionResult Logout()
    {

        
        HttpContext.Session.Remove("UserInfo");
        HttpContext.Session.Remove("Cart");

       
        Response.Cookies.Delete("UserInfo");

       
        return RedirectToAction("Index", "Home");
    }
}
