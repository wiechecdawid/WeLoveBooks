using Microsoft.AspNetCore.Identity;
using WeLoveBooks.DataAccess.Data.Seeder;
using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.Mvc.Middleware;

public static class DbInitializer
{
    public static async Task Initialize(WebApplicationBuilder builder, WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        await CreateRoles(builder, scope);
        await ConfigureSiteAdmin(scope, builder.Configuration);
        // TODO: Check how to seed DB in .NET6
        await Seed(scope);
    }

    private static async Task Seed(IServiceScope scope)
    {
        var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
        await seeder.Initialize();
    }
    private static async Task CreateRoles(WebApplicationBuilder builder, IServiceScope scope)
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        List<IdentityRole> roles = new()
        {
            new IdentityRole { Name = builder.Configuration["RoleNames:SiteAdmin"] },
            new IdentityRole { Name = builder.Configuration["RoleNames:GroupAdmin"] },
            new IdentityRole { Name = builder.Configuration["RoleNames:GroupUser"] },
            new IdentityRole { Name = builder.Configuration["RoleNames:RegularUser"] },
        };

        foreach (var role in roles)
        {
            if (await roleManager.RoleExistsAsync(role.Name)) continue;
            var result = await roleManager.CreateAsync(role);
            if (!result.Succeeded)
                throw new Exception($"Could not create {role.Name} role.");
        }
    }

    private static async Task ConfigureSiteAdmin(IServiceScope scope, IConfiguration cfg)
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        if (await userManager.FindByEmailAsync(cfg["SuperAdmin:Email"]) is not null)
            return;
        if (!await roleManager.RoleExistsAsync(cfg["RoleNames:SiteAdmin"]))
            throw new Exception($"The role '{cfg["RoleNames:SiteAdmin"]}' has not been set up.");

        AppUser user = new()
        {
            FirstName = "Site",
            LastName = "Admin",
            UserName = cfg["SuperAdmin:UserName"],
            Email = cfg["SuperAdmin:Email"]
        };

        //await userManager.AddPasswordAsync(user, cfg["SuperAdmin:Password"]);
        await userManager.CreateAsync(user, cfg["SuperAdmin:Password"]);
        await userManager.AddToRoleAsync(user, cfg["RoleNames:SiteAdmin"]);
    }
}
