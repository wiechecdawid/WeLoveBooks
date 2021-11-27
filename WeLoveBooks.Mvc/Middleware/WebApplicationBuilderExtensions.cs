using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeLoveBooks.DataAccess.Data;
using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.Mvc.Middleware;

public static class WebApplicationBuilderExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentity<AppUser, IdentityRole>(cfg =>
        {
            cfg.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<AppDbContext>();

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
