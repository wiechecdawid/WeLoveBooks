using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Data.Seeder;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.Mvc.Middleware;
using WeLoveBooks.Mvc.Services.ObjectToModelConverter;
using WeLoveBooks.Mvc.Services.PhotoBrokerHttpClient;
using WeLoveBooks.Mvc.Services.ReviewService;
using WeLoveBooks.Mvc.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISeeder, Seeder>();
builder.Services.AddScoped<IObjectToModelConverter<Book, BookViewModel>, BookToViewModelConverter>();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(
   builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddAuthorization(options =>
    options.AddPolicy("SiteAdmin", policy =>
        policy.RequireRole(builder.Configuration["RoleNames:SiteAdmin"])  
));
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddHttpClient("PhotoBroker", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PhotoBroker:Url"]);
    //client.DefaultRequestHeaders.Host = builder.Configuration["PhotoBroker:Host"];
});
builder.Services.AddScoped<IPhotoBrokerHttpClient, PhotoBrokerHttpClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

await DbInitializer.Initialize(builder, app);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseNodeModules(builder.Environment.WebRootPath);
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();