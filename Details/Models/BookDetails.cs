using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Details.Models
{
    [DataContract]
    public class BookDetails
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public int Pages { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        [JsonPropertyName("isbn-10")]
        public string Isbn10 { get; set; }
        [JsonPropertyName("isbn-13")]
        public string Isbn13 { get; set; }
    }
}