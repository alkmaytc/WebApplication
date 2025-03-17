var builder = WebApplication.CreateBuilder(args);

// MVC ve Session deste�ini ekle
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session s�resi (30 dakika)
    options.Cookie.HttpOnly = true; // G�venlik i�in HttpOnly
    options.Cookie.IsEssential = true; // �erezler devre d��� b�rak�lsa bile �al��mas�n� sa�lar
});

var app = builder.Build();

// Hata y�netimi
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
