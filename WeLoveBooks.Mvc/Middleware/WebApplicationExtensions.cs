using Microsoft.Extensions.FileProviders;

namespace WeLoveBooks.Mvc.Middleware;

public static class WebApplicationExtensions
{
    public static WebApplication UseNodeModules(this WebApplication app, string root)
    {
        var path = Path.Combine(root, "node_modules");

        PhysicalFileProvider provider = new(path);

        StaticFileOptions options = new();
        options.RequestPath = "/node_modules";
        options.FileProvider = provider;

        app.UseStaticFiles(options);

        return app;
    }
}
