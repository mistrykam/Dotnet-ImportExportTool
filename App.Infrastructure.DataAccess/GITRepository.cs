using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

public class GITRepository : IRepository
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<IEnumerable<T>> Get<T>(ApiRequest apiRequest)
    {
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(apiRequest.Accept));
        client.DefaultRequestHeaders.Add("User-Agent", apiRequest.UserAgent);

        var streamTask = client.GetStreamAsync(apiRequest.Uri);
        var repositories = await JsonSerializer.DeserializeAsync<List<T>>(await streamTask);

        return repositories;
    }
}