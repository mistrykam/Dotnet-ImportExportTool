using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Domain.Repository
{
    public interface IFileExportRepository
    {
        Task WriteToCSVFileAsync<T>(IEnumerable<T> data, string filePath);
    }
}