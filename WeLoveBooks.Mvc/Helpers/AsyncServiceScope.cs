namespace WeLoveBooks.Mvc.Helpers;

public class AsyncServiceScope
{
    private readonly IServiceScope _serviceScope;
    public AsyncServiceScope(IServiceScope serviceScope)
    {
        _serviceScope = serviceScope ?? throw new ArgumentNullException(nameof(serviceScope));
    }

    public IServiceProvider ServiceProvider => _serviceScope.ServiceProvider;
    public void Dispose() => _serviceScope.Dispose();

    public ValueTask DisposeAsync()
    {
        if (_serviceScope is IAsyncDisposable ad)
        {
            return ad.DisposeAsync();
        }
        _serviceScope.Dispose();

        // ValueTask.CompletedTask is only available in net5.0 and later.
        return default;
    }
}
