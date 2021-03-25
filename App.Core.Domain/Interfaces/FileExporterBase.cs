using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain.Interfaces
{
    public abstract class FileExporterBase
    {
        public abstract void Start();

        public abstract Task<bool> ReadDataAsync();

        public abstract Task<bool> ExportDataAsync();

        public abstract void End();

        public async Task ExecuteAsync()
        {
            try
            {
                Start();
                bool result = await ReadDataAsync();

                if (result == true)
                    await ExportDataAsync();
                End();
            }
            catch (Exception ex)
            {
                // TODO: Log this error
                Console.WriteLine($"Error Occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Write the data to the specified directory
        /// 
        /// NOTE: This method will throw an exception and should be handled by the caller
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected static async Task WriteToCSVFile<T>(IEnumerable<T> data, string filePath)
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