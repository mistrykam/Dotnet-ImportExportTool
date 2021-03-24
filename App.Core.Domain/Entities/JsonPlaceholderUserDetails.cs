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
        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonPropertyName("suite")]
        public string Suite { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("zipcode")]
        public string ZipCode { get; set; }

        [JsonPropertyName("geo")]
        public Geo Geo { get; set; }
    }

    public class Geo
    {
        [JsonPropertyName("lat")]
        public string Lattitude { get; set; }

        [JsonPropertyName("lng")]
        public string Longitude { get; set; }
    }

    public class Company
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("catchPhrase")]
        public string CatchPhrase { get; set; }

        [JsonPropertyName("bs")]
        public string BS { get; set; }
    }
}