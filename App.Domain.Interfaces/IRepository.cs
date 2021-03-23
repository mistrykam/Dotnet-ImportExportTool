using App.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository
{
    Task<IEnumerable<T>> Get<T>(ApiRequest apiRequest);
}