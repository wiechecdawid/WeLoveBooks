using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.Mvc.Middleware;

public static class WebApplicationBuilderExtensions
{
    public static async Task CreateRoles(this WebApplicationBuilder builder, RoleManager<IdentityRole> roleManager)
    {
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

    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentity<AppUser, IdentityRole>(cfg =>
        {
            cfg.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Token:SecretKey"]));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
