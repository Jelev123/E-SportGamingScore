using E_SportGamingScore.Core.Contracts.Matches;
using E_SportGamingScore.Core.Contracts.XML;
using E_SportGamingScore.Core.Services.Matches;
using E_SportGamingScore.Core.Services.XML;
using E_SportGamingScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Transient);

builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IXmlService, XMLService>();
builder.Services.AddTransient<IMatches, MatchesService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
