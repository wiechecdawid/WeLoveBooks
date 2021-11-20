using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Data.Seeder;
using WeLoveBooks.Mvc.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(
   builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

await builder.CreateRoles(
    app.Services.GetRequiredService<RoleManager<IdentityRole>>());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

await app.ConfigureSiteAdmin(builder.Configuration);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

Seeder seeder = new(app.Services, builder.Configuration);

app.Run();