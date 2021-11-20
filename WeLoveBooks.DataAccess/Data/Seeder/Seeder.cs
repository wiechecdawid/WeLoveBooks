using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.DataAccess.Data.Seeder;

public class Seeder: ISeeder
{
    private readonly IServiceProvider _services;
    private readonly IConfiguration _config;
    public Seeder(IServiceProvider services, IConfiguration config)
    {
        _services = services;
        _config = config;
    }

    public async Task Initialize()
    {
        using var context = _services.GetService<AppDbContext>();
        await context!.Database.MigrateAsync();
    }

    public async Task Seed()
    {
        var userManager = _services.GetService<UserManager<AppUser>>();
        using var context = _services.GetService<AppDbContext>();

        if(userManager.Users.Count() < 2)
        {
            AppUser user = new()
            {
                UserName = "Asia W",
                Email = "admin@test.com",
            };

            await userManager.CreateAsync(user, "Test");
            await userManager.AddToRoleAsync(user, _config["RoleNames:RegularUser"]);
        }

        if(!context.Authors.Any())
        {
            Author author1 = new()
            {
                FirstName = "Edgar Allan",
                LastName = "Poe",
                DateOfBirth = new DateTime(1809, 1, 19)
            };

            Author author2 = new()
            {
                FirstName = "Jeff",
                LastName = "VanderMeer",
                DateOfBirth = new DateTime(1968, 2, 7)
            };

            await context.Authors.AddRangeAsync(author1, author2);
        }

        if (!context.Books.Any())
        {
            var a1 = await context.Authors.Where(a => a.FirstName == "Jeff").FirstOrDefaultAsync();
            var a2 = await context.Authors.Where(a => a.LastName == "Poe").FirstOrDefaultAsync();

            Book book1 = new()
            {
                AuthorId = a1.Id,
                Title = "Unicestwienie",
                CreatedDate = new DateTime(2014, 2, 4)
            };

            Book book2 = new()
            {
                AuthorId = a1.Id,
                Title = "Ujarzmienie",
                CreatedDate = new DateTime(2014, 5, 6)
            };

            Book book3 = new()
            {
                AuthorId = a2.Id,
                Title = "Kruk",
                CreatedDate = new DateTime(1845, 1, 29)
            };

            await context.Books.AddRangeAsync(book1, book2, book3);
        }

        if (!context.Reviews.Any())
        {
            var b1 = await context.Books.Where(a => a.Title == "Unicestwienie").FirstOrDefaultAsync();
            var b2 = await context.Books.Where(a => a.Title == "Ujarzmienie").FirstOrDefaultAsync();
            var b3 = await context.Books.Where(a => a.Title == "Kruk").FirstOrDefaultAsync();

            Review review1 = new()
            {
                BookId = b1.Id,
                Title = "Unicestwienie",
                CreatedDate = DateTime.Now
            };

            Review review2 = new()
            {
                BookId = b2.Id,
                Title = "Ujarzmienie",
                CreatedDate = DateTime.Now
            };

            Review review3 = new()
            {
                BookId = b3.Id,
                Title = "Kruk",
                CreatedDate = DateTime.Now
            };

            await context.Reviews.AddRangeAsync(review1, review2, review3);
        }

        await context.SaveChangesAsync();
    }
}