var builder = WebApplication.CreateBuilder(args);

// MVC ve Session desteðini ekle
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session süresi (30 dakika)
    options.Cookie.HttpOnly = true; // Güvenlik için HttpOnly
    options.Cookie.IsEssential = true; // Çerezler devre dýþý býrakýlsa bile çalýþmasýný saðlar
});

var app = builder.Build();

// Hata yönetimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // ? Session Middleware aktif hale getirildi
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
