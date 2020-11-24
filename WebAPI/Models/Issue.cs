using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class Issue{
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

    }
}
