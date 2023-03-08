using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using RecipeAZ.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Authentication;
using RecipeAZ.Data;
using RecipeAZ.Areas.Identity.Data;
using RecipeAZ.Areas.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");

builder.Services.AddDbContext<IdentityContext>(opts => {
    opts.UseSqlServer(connectionString);
    opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddDbContext<DataContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:RecipeConnection"]!);
    opts.EnableSensitiveDataLogging(true);
});

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<IdentityContext>();


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<AppUser>>();

builder.Services.AddMudServices();


var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseAuthentication();
app.UseAuthorization();


var context = app.Services.CreateScope().ServiceProvider
    .GetRequiredService<DataContext>();

SeedData.SeedDatabase(context);/*, userManager);*/

app.Run();
