using System;
using System.Threading.Tasks;

namespace App.Core.Domain.Interfaces
{
    public abstract class ImportExportBase
    {
        public abstract void Start();

        public abstract Task<bool> ReadDataAsync();

        public abstract Task<bool> ExportDataAsync();

        public abstract void End();

        public async Task ExecuteAsync()
        {
            Start();

            bool result = await ReadDataAsync();

            if (result == true)
                await ExportDataAsync();

            End();
        }
    }
}