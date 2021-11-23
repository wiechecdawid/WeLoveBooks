namespace WeLoveBooks.Mvc.Middleware;

public static class ServiceProviderExtensions
{
    public static AsyncServiceScope CreateAsyncScope(this IServiceProvider provider) => new AsyncServiceScope(provider.CreateScope());
}
