using App.Core.Domain.Entities;
using App.Core.Domain.Repository;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Infrastructure.DataAccess
{
    public class JsonPlaceholderRepository : IJsonPlaceholderRepository
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<IEnumerable<JsonPlaceholderUserDetails>> GetAsync()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(apiRequest.Accept));
            client.DefaultRequestHeaders.Add("User-Agent", "Import Export Tool");

            var streamTask = await client.GetStreamAsync("http://jsonplaceholder.typicode.com/users");
            var list = await JsonSerializer.DeserializeAsync<List<JsonPlaceholderUserDetails>>(streamTask);

            return list;
        }
    }
}
