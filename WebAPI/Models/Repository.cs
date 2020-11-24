using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class Repository{
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("html_url")]
        public string Url { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("issues_url")]
        public string IssueUrl { get; set; }

    //    public List<Issue> Issues { get; set; }
    }
}