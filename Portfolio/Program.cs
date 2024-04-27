using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<_context>(b =>
{
    b.UseSqlServer(builder.Configuration.GetConnectionString("con"));
});

builder.Services.AddSession(a =>
{
	a.IdleTimeout = TimeSpan.FromSeconds(900); //15min
	a.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
