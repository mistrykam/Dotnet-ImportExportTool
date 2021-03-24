namespace App.Core.Domain.Entities
{
    public class AppSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string LogFilePath { get; set; }

        public string GitUri { get; set; }
        public string GitAccept { get; set; }
        public string GitUserAgent { get; set; }

        public string JsonPlaceholderUri { get; set; }
        public string JsonPlaceholderAccept { get; set; }
        public string JsonPlaceholderUserAgent { get; set; }
    }
}