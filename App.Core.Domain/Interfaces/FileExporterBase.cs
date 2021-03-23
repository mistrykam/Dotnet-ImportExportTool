namespace App.Core.Domain.Interfaces
{
    public abstract class FileExporterBase
    {
        public abstract void Start();

        public abstract void ReadData();

        public abstract void ExportData();

        public abstract void End();

        public void Execute()
        {
            Start();
            ReadData();
            ExportData();
            End();
        }
    }
}