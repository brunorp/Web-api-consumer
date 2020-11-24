using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly HttpClient _client;
        public RepositoryService(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json")
            );
            _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        }

        public async Task<List<Repository>> RequestApi(string company){
           
            var streamTask = _client.GetStreamAsync($"https://api.github.com/orgs/{company}/repos");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
            /*foreach(var repo in repositories){
                var t = client.GetStreamAsync($"https://api.github.com/repos/{company}/{repo.Name}/issues");
                repo.Issues = await JsonSerializer.DeserializeAsync<List<Issue>>(await t); 
            }*/
            return repositories;
        }
    }
}