using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using RecipeAZ.Models;
using RecipeAZ.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var connectionString = builder.Configuration.GetConnectionString("RecipeConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");

builder.Services.AddDbContextFactory<DataContext>(opts => {
    opts.UseSqlServer(connectionString);
    opts.EnableSensitiveDataLogging(true);
    opts.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
});


builder.Services.AddIdentity<AppUser, IdentityRole>( opts => {
    opts.SignIn.RequireConfirmedAccount = true;
    opts.SignIn.RequireConfirmedEmail = true;
    opts.Tokens.EmailConfirmationTokenProvider = "Email";
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireUppercase= false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders()
    .AddTokenProvider<EmailTokenProvider<AppUser>>("Email");

builder.Services.AddScoped<UserManager<AppUser>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddTransient<IEmailSender, SendGridEmailSender>();
builder.Services.AddSingleton<EditService>();
builder.Services.AddScoped<TextProcessing>();
builder.Services.AddScoped<NavHelperService>();
builder.Services.AddScoped<RecipeService>();
builder.Services.AddScoped<JsonDbService>();
builder.Services.AddScoped<MailService>();

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

var userManager = app.Services.CreateScope().ServiceProvider
    .GetRequiredService<UserManager<AppUser>>();
var roleManager = app.Services.CreateScope().ServiceProvider
    .GetRequiredService<RoleManager<IdentityRole>>();
var dataContext = app.Services.CreateScope().ServiceProvider
    .GetRequiredService<DataContext>();


await SeedData.InitializeAsync(userManager, roleManager, dataContext);/*, userManager);*/

app.Run();
