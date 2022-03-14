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
        await using var context = _services.GetService<AppDbContext>();
        await context!.Database.MigrateAsync();
        await Seed(context);
    }

    private async Task Seed(AppDbContext context)
    {
        var role_RegularUser = _config["RoleNames:RegularUser"];
        var userManager = _services.GetService<UserManager<AppUser>>();

        if(userManager.Users.Count() < 2)
        {
            AppUser user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "testUser@test.com",
                UserName = "testUser@test.com",
                FirstName = "Jan",
                LastName = "Kowalski"
            };

            var result = await userManager.CreateAsync(user, "P@ssvv0rd");

            if (result.Succeeded)
            {
                var savedUser = await userManager.FindByEmailAsync(user.Email);
                await userManager.AddToRoleAsync(savedUser, role_RegularUser);
            }
        }

        if(!context.Authors.Any())
        {
            Author author1 = new()
            {
                FirstName = "Edgar Allan",
                LastName = "Poe",
                DateOfBirth = new DateTime(1809, 1, 19),
                Bio = "First sentence. Second Sentence. Third Sentence.",
                Books = new List<Book>()
            };

            Author author2 = new()
            {
                FirstName = "Jeff",
                LastName = "VanderMeer",
                DateOfBirth = new DateTime(1968, 2, 7),
                Bio = "First sentence. Second sentence. Third Sentence.",
                Books = new List<Book>()
            };

            await context.Authors.AddRangeAsync(author1, author2);
        }

        if (!context.Books.Any())
        {
            var a1 = context.Authors.Local.FirstOrDefault(a => a.LastName == "VanderMeer");
            var a2 = context.Authors.Local.FirstOrDefault(a => a.LastName == "Poe");

            Book book1 = new()
            {
                AuthorId = a1.Id,
                Author = a1,
                Title = "Unicestwienie",
                CreatedDate = new DateTime(2014, 2, 4),
                Description = "First sentence. Second sentence. Third Sentence.",
                Reviews = new List<Review>()
            };

            Book book2 = new()
            {
                AuthorId = a1.Id,
                Author = a1,
                Title = "Ujarzmienie",
                CreatedDate = new DateTime(2014, 5, 6),
                Description = "First sentence. Second sentence. Third Sentence.",
                Reviews = new List<Review>()
            };

            Book book3 = new()
            {
                AuthorId = a2.Id,
                Author = a2,
                Title = "Kruk",
                CreatedDate = new DateTime(1845, 1, 29),
                Description = "First sentence. Second sentence. Third Sentence.",
                Reviews = new List<Review>()
            };

            await context.Books.AddRangeAsync(book1, book2, book3);
        }

        if (!context.Reviews.Any())
        {
            var b1 = context.Books.Local.Where(a => a.Title == "Unicestwienie").ToList().FirstOrDefault();
            var b2 = context.Books.Local.Where(a => a.Title == "Ujarzmienie").ToList().FirstOrDefault();
            var b3 = context.Books.Local.Where(a => a.Title == "Kruk").ToList().FirstOrDefault();

            Review review1 = new()
            {
                BookId = b1.Id,
                Book = b2,
                Title = "Unicestwienie",
                CreatedDate = DateTime.Now,
                AppUser = context.Users.FirstOrDefault(),
                BookRate = new BookRate()
                {
                    Id = Guid.NewGuid(),
                    Verdict = Verdict.VeryGood
                },
                Content = "Bardzo dobra książka. Polecam ją każdemu fanowi Jeffa. Pozycja obowiązkowa"
            };

            Review review2 = new()
            {
                BookId = b2.Id,
                Title = "Ujarzmienie",
                CreatedDate = DateTime.Now,
                AppUser = context.Users.FirstOrDefault()!,
                BookRate = new BookRate()
                {
                    Id = Guid.NewGuid(),
                    Verdict = Verdict.VeryBad
                },
                Content = "Nie polecam. Przez pierwsze pół książki nic się nie dzieje, a potem dla odmiany nic się nie dzieje. Zmarnowany wieczór."
            };

            Review review3 = new()
            {
                BookId = b3.Id,
                Title = "Kruk",
                CreatedDate = DateTime.Now,
                AppUser = context.Users.FirstOrDefault()!,
                BookRate = new BookRate()
                {
                    Id = Guid.NewGuid(),
                    Verdict = Verdict.Neutral
                },
                Content = "Przeciętna książka. Na szczęście nie jest zbyt długa. Można poczytać."
            };

            await context.Reviews.AddRangeAsync(review1, review2, review3);
        }

        await context.SaveChangesAsync();
    }
}