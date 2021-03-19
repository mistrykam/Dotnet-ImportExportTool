using App.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWebApiRequest
{
    Task<IEnumerable<T>> ExecuteRequest<T>(ApiRequest apiRequest);
}