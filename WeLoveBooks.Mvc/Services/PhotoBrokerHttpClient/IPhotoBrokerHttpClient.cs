using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Services.PhotoBrokerHttpClient;

public interface IPhotoBrokerHttpClient
{
    Task<HttpResponseMessage> SendAsync(PhotoFormViewModel model);
}
