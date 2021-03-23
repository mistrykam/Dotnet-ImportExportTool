using App.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Domain.Interfaces
{
    public interface IGitRepository
    {
        Task<IEnumerable<GitRepoDetails>> Get(GitApiRequest apiRequest);
    }
}