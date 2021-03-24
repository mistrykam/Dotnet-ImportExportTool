using System;
using System.Text.Json.Serialization;

namespace App.Core.Domain.Entities
{
    // using https://json2csharp.com/ to convert JSON response to C# object

    public class JsonPlaceholderUserDetails
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("Phone")]
        public string phone { get; set; }

        [JsonPropertyName("website")]
        public Uri WebSite { get; set; }

        [JsonPropertyName("company")]
        public Company Company { get; set; }
    }

    public class Address
    {
        public string street { get; set; }
        public string suite { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public Geo geo { get; set; }
    }

    public class Geo
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class Company
    {
        public string name { get; set; }
        public string catchPhrase { get; set; }
        public string bs { get; set; }
    }
}