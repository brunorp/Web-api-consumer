using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class Repository{
        [Required(ErrorMessage = "Campo \"Name\" é obrigatório")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("html_url")]
        public Uri Url { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("issues_url")]
        public Uri IssueUrl { get; set; }

    //    public List<Issue> Issues { get; set; }
    }
}