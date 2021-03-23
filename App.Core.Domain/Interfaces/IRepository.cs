using App.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Domain.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<T>> Get<T>(ApiRequest apiRequest);
    }
}