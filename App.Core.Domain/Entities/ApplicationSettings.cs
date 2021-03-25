namespace App.Core.Domain.Entities
{
    public class AppSettings
    {
        // Git Import/Export
        public string GitUri { get; set; }
        public string GitAccept { get; set; }
        public string GitUserAgent { get; set; }
        public string GitExportFilePath { get; set; }

        // Json Placeholder Import/Export
        public string JsonPlaceholderUri { get; set; }
        public string JsonPlaceholderAccept { get; set; }
        public string JsonPlaceholderUserAgent { get; set; }
        public string JsonExportFilePath { get; set; }
    }
}