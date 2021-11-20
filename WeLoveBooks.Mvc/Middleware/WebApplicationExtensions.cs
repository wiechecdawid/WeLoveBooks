using Microsoft.AspNetCore.Identity;
using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.Mvc.Middleware
{
    public static class WebApplicationExtensions
    {
        public static async Task ConfigureSiteAdmin(this WebApplication app, IConfiguration cfg)
        {
            var roleManager = app.Services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = app.Services.GetRequiredService<UserManager<AppUser>>();

            if (await userManager.FindByEmailAsync(cfg["RoleNames:SiteAdmin"]) is not null)
                return;
            if (!await roleManager.RoleExistsAsync(cfg["RoleNames:SiteAdmin"]))
                throw new Exception($"The role '{cfg["RoleNames:SiteAdmin"]}' has not been set up.");

            AppUser user = new()
            {
                Email = cfg["SuperAdmin:Email"]
            };

            //await userManager.AddPasswordAsync(user, cfg["SuperAdmin:Password"]);
            await userManager.CreateAsync(user, cfg["SuperAdmin:Password"]);
            await userManager.AddToRoleAsync(user, cfg["RoleNames:SiteAdmin"]);
        }
    }
}
