using App.Core.Domain.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.ExportData
{
    public class FileExportRepository : IFileExportRepository
    {
        /// <summary>
        /// Write the data to the specified directory
        /// 
        /// NOTE: This method will throw an exception and should be handled by the caller
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task WriteToCSVFileAsync<T>(IEnumerable<T> data, string filePath)
        {
            try
            {
                IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
                string header = string.Join(",", props.ToList().Select(x => x.Name));

                await File.WriteAllTextAsync(filePath, header + "\n", Encoding.UTF8);

                IEnumerable<string> list = data.Select(x => string.Join(",", x));

                await File.AppendAllLinesAsync(filePath, list, Encoding.UTF8);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new Exception($"Directory {filePath} not found", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new Exception($"Could not access the directory {filePath}, permission denied", ex);
            }
            catch (IOException ex)
            {
                throw new Exception("Error during file write", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred", ex);
            }
        }
    }
}
