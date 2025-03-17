using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
            // Kullanıcı adını ve soyadını Session’a kaydet
            HttpContext.Session.SetString("UserName", firstName + " " + lastName);

            // Kullanıcı adını ve soyadını Cookie olarak kaydet (1 ay geçerli)
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Append("UserInfo", firstName + " " + lastName, options);

            // Ana sayfaya yönlendir
            return RedirectToAction("Index", "Home");
        }

        // Hata mesajı, giriş bilgileri eksikse
        ViewBag.Error = "Lütfen adınızı ve soyadınızı giriniz!";
        return View("Index");  // Eğer form eksikse tekrar Login sayfasına döner
    }


    // 4️⃣ Çıkış işlemi
    public IActionResult Logout()
    {
        // 1️⃣ Session'ı temizle
        HttpContext.Session.Clear();

        // 2️⃣ Cookie'yi temizle
        Response.Cookies.Delete("UserInfo");

        // 3️⃣ Ana sayfaya yönlendir
        return RedirectToAction("Index", "Home");
    }

}
