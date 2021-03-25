namespace App.Core.Domain.Entities
{
    // using https://json2csharp.com/ to convert JSON response to C# object

    public class JsonPlaceholderApiRequest
    {
        public string Accept { get; set; }
        public string UserAgent { get; set; }
        public string Uri { get; set; }
    }
}
