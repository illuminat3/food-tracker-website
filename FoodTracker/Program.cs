using FoodTracker.DbContext;
using FoodTracker.Service.DataServices;
using FoodTracker.Service.DataServices.Abstraction;
using FoodTracker.Service.DataServices.DataAccess;
using FoodTracker.Service.DataServices.DataAccess.Abstraction;
using FoodTracker.Service.FunctionalServices;
using FoodTracker.Service.FunctionalServices.Abstraction;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configure EF Core to use SQLite
builder.Services.AddDbContext<FoodTrackerContext>(options =>
    options.UseSqlite("Data Source=database/database.db"));

// Register application services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserDataAccess, UserDataAccess>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddSingleton<IHashService, HashService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Configure logging
builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});

// Configure Kestrel to use port 500 - must be http or causes issues
builder.WebHost.UseUrls("http://*:500");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();