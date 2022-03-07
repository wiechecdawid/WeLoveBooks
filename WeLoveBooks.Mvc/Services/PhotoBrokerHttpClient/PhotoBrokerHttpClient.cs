using Microsoft.AspNetCore.WebUtilities;
using WeLoveBooks.Mvc.ViewModels;
using System.Net.Http.Headers;

namespace WeLoveBooks.Mvc.Services.PhotoBrokerHttpClient;

public class PhotoBrokerHttpClient : IPhotoBrokerHttpClient
{
    private readonly HttpClient _httpClient;
    private const string photoEndpoint = "api/photo";

    public PhotoBrokerHttpClient(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("PhotoBroker");
    }

    public async Task<HttpResponseMessage> SendAsync(PhotoFormViewModel model)
    {
        var requestUrl = PrepareRequestUrl(model.Type, model.Id);

        var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

        using var content = CreateContent(model.File);
        request.Content = content;

        var result = await _httpClient.SendAsync(request);
        result.EnsureSuccessStatusCode();

        return result;
    }

    private MultipartFormDataContent CreateContent(IFormFile file)
    {
        if (file is null || file.Length <= 0)
            throw new ArgumentNullException("File is null or empty");

        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)
            .FileName?.Trim('"');

       var content = new MultipartFormDataContent();
        content.Add(new StreamContent(file.OpenReadStream())
        {
            Headers =
            {
                ContentLength = file.Length,
                ContentType = new MediaTypeHeaderValue(file.ContentType)
            }
        }, "File", fileName);

        return content;
    }

    private string PrepareRequestUrl(int type, string id)
    {        
        var queryString = new Dictionary<string, string?>();

        queryString["id"] = id;
        queryString["type"] = type.ToString();

        return QueryHelpers.AddQueryString(photoEndpoint, queryString);
    }
}
