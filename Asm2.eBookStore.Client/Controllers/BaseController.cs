using System.Text;
using System.Text.Json.Serialization;
using Asm2.eBookStore.Api.Dto.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Client;
using Newtonsoft.Json;

namespace Asm2.eBookStore.Client.Controllers;

public class BaseController : Controller
{
    private readonly DataServiceContext _context;
    private readonly HttpClient _httpClient;

    public BaseController(HttpClient httpClient, DataServiceContext context)
    {
        _httpClient = httpClient;
        _context = context;
        _httpClient.BaseAddress = new Uri("http://localhost:5113/api");
    }

    protected DataServiceQuery<T> QueryOf<T>()
        where T : IODataEntity => _context.CreateQuery<T>(T.EntitySet);

    public async Task<(HttpResponseMessage, T?)> PostAsync<T>(
        IQueryable<T> query,
        object payloadObj
    )
    {
        AddAuthCookie();
        var payload = JsonConvert.SerializeObject(payloadObj);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(RequestUri(query), content);
        T? resultContent = default;
        if (response.IsSuccessStatusCode)
        {
            resultContent = JsonConvert.DeserializeObject<T>(
                response.Content.ReadAsStringAsync().Result
            );
        }
        return (response, resultContent);
    }

    public async Task<(HttpResponseMessage, T?)> PostAsync<T>(string uri, object payloadObj)
    {
        AddAuthCookie();
        var payload = JsonConvert.SerializeObject(payloadObj);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(uri, content);
        T? resultContent = default;
        if (response.IsSuccessStatusCode)
        {
            resultContent = JsonConvert.DeserializeObject<T>(
                response.Content.ReadAsStringAsync().Result
            );
        }
        return (response, resultContent);
    }

    public async Task<HttpResponseMessage> DeleteAsync<T>(IQueryable<T> query)
    {
        AddAuthCookie();
        var response = await _httpClient.DeleteAsync(RequestUri(query));
        return response;
    }

    public async Task<HttpResponseMessage> PutAsync<T>(IQueryable<T> query, object payloadObj)
    {
        AddAuthCookie();
        var payload = JsonConvert.SerializeObject(payloadObj);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(RequestUri(query), content);
        return response;
    }

    public async Task<(HttpResponseMessage, ODataResponse<T>?)> GetAsync<T>(
        IQueryable<T> query,
        int page = 1,
        int pageSize = 1
    )
    {
        AddAuthCookie();

        query = query.Skip((page - 1) * pageSize).Take(pageSize);
        var response = await _httpClient.GetAsync(RequestUri(query));
        if (!response.IsSuccessStatusCode)
            return (response, default);
        var content = JsonConvert.DeserializeObject<ODataResponse<T>>(
            response.Content.ReadAsStringAsync().Result
        );
        return (response, content);
    }

    public async Task<(HttpResponseMessage, T?)> GetSingleAsync<T>(IQueryable<T> query)
    {
        var (response, content) = await GetAsync(query);
        return (response, content!.Value.FirstOrDefault());
    }

    private static Uri RequestUri<T>(IQueryable<T> query)
    {
        var requestUri = new Uri(query.ToString()!, UriKind.Absolute);
        return requestUri;
    }

    private void AddAuthCookie()
    {
        var cookie = HttpContext.Session.GetString("AuthCookie");
        if (cookie == null)
            return;
        _httpClient.DefaultRequestHeaders.Add("Cookie", cookie);
    }
}

public class ODataResponse<T>
{
    [JsonPropertyName("@odata.context")]
    public string Context { get; set; } = "";

    [JsonPropertyName("value")]
    public List<T> Value { get; set; } = new();

    [JsonProperty("@odata.count")]
    public int TotalCount { get; set; }
}
