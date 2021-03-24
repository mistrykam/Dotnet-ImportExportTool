using App.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Domain.Repository
{
    public interface IJsonPlaceholderRepository
    {
        Task<IEnumerable<JsonRepoDetails>> GetAsync();
    }
}