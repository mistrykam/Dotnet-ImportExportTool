using App.Core.Domain.Entities;
using App.Core.Domain.Repository;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Infrastructure.DataAccess
{
    public class GitRepository : IGitRepository
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<IEnumerable<GitRepoDetails>> GetAsync(GitApiRequest apiRequest)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(apiRequest.Accept));
            client.DefaultRequestHeaders.Add("User-Agent", apiRequest.UserAgent);

            var streamTask = await client.GetStreamAsync(apiRequest.Uri);
            var list = await JsonSerializer.DeserializeAsync<List<GitRepoDetails>>(streamTask);

            return list;
        }
    }
}