using MenuExplorer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IMenuService, JsonMenuService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Menu/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Registered first so generated links come out as /Menu?category=...
// rather than /?category=...
app.MapControllerRoute(
    name: "menu",
    pattern: "Menu/{action=Index}/{id?}",
    defaults: new { controller = "Menu" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Menu}/{action=Index}/{id?}");

app.Run();
