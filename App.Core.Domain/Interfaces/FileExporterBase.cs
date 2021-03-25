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

        public abstract Task ReadDataAsync();

        public abstract Task ExportDataAsync();

        public abstract void End();

        public async Task ExecuteAsync()
        {
            try
            {
                Start();
                await ReadDataAsync();
                await ExportDataAsync();
                End();
            }
            catch (Exception ex)
            {
                // TODO: Log this error
                Console.WriteLine($"Error Occurred: {ex.Message}");
            }
        }

        protected static async Task WriteToCSVFile<T>(IEnumerable<T> data, string filePath)
        {
            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
            string header = string.Join(",", props.ToList().Select(x => x.Name));

            await File.WriteAllTextAsync(filePath, header + "\n", Encoding.UTF8);

            IEnumerable<string> list = data.Select(x => string.Join(",", x));

            await File.AppendAllLinesAsync(filePath, list, Encoding.UTF8);
        }
    }
}