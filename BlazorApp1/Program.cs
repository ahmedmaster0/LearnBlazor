using BlazorApp1;
using BlazorApp1.Authentication;
using BlazorApp1.Data;
using BlazorApp1.SeedData;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(op =>
{
    op.SignIn.RequireConfirmedAccount = false;
    op.SignIn.RequireConfirmedPhoneNumber = false;
}).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthentication>();

builder.Services.AddSingleton<WeatherForecastService>();

//Authentication Register
//builder.Services.AddAuthentication(op =>
//{
//    op.RequireAuthenticatedSignIn = false;
//})
//    .AddCookie(op =>
//{
//    op.Cookie.Name = "cookieToken";
//    op.LoginPath = "/login";
//    op.AccessDeniedPath = "/accessDenied";
//    op.Cookie.MaxAge = TimeSpan.FromMinutes(30);
//});
//builder.Services.AddAuthorization();


builder.Services.AddSweetAlert2();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


using (var scope = app.Services.CreateScope())
{
    var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedRoles.AddRoles(_roleManager);
}

app.Run();
