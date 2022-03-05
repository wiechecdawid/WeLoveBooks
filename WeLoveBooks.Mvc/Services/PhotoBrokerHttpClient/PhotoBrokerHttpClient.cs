using Microsoft.AspNetCore.WebUtilities;
using WeLoveBooks.Mvc.ViewModels;

namespace WeLoveBooks.Mvc.Services.PhotoBrokerHttpClient;

public class PhotoBrokerHttpClient : IPhotoBrokerHttpClient
{
    private readonly HttpClient _httpClient;

    public PhotoBrokerHttpClient(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("PhotoBroker");
    }

    public async Task<HttpResponseMessage> SendAsync(PhotoFormViewModel model)
    {
        var requestUrl = PrepareRequestUrl(model.Type, model.Id);

        var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);

        var formData = new MultipartFormDataContent();
        formData.Add(new ByteArrayContent(ReadFile(model.File)));
        request.Content = formData;

        var result = await _httpClient.SendAsync(request);

        return result;
    }

    private byte[] ReadFile(IFormFile file)
    {
        byte[] data;

        if (file is null || file.Length == 0)
            throw new ArgumentNullException("The file cannot be null");

        using var reader = new BinaryReader(file.OpenReadStream());
        data = reader.ReadBytes((int)file.OpenReadStream().Length);

        return data;
    }

    private string PrepareRequestUrl(int type, string id)
    {        
        var queryString = new Dictionary<string, string?>();

        queryString["type"] = type.ToString();
        queryString["id"] = id;

        return QueryHelpers.AddQueryString(queryString);
    }
}
