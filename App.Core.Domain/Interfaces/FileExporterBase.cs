using System.Threading.Tasks;

namespace App.Core.Domain.Interfaces
{
    public abstract class FileExporterBase
    {
        public abstract void Start();

        public abstract Task ReadDataAsync();

        public abstract void ExportData();

        public abstract void End();

        public async Task ExecuteAsync()
        {
            Start();
            await ReadDataAsync();
            ExportData();
            End();
        }
    }
}