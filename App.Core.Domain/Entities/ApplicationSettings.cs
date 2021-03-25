namespace App.Core.Domain.Entities
{
    public class AppSettings
    {
        public ApiSettings GitSettings { get; private set; } = new ApiSettings();
        public ApiSettings JsonPlaceholderSettings { get; private set; } = new ApiSettings();
    }

    public class ApiSettings
    {
        public string Uri { get; set; }
        public string Accept { get; set; }
        public string UserAgent { get; set; }
        public string ExportFilePath { get; set; }
    }
}