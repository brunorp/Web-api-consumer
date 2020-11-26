using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Public;

namespace WebAPI.Services
{
    public class RepositoryService : IRepositoryService
    {
        public async Task<List<Repository>> RequestApi(HttpClient _client, string company){
            ClientOptions(_client);
            
            try{
                var streamTask = _client.GetStreamAsync($"https://api.github.com/orgs/{company}/repos");
                var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
                foreach(var repo in repositories){
                    if(repo.Name == null || repo.Url == null)
                        throw new HttpRequestException("API connection failed");
                }    
                return repositories;
            }catch(Exception){
                throw new HttpRequestException("API connection failed");
            }            
        }

        private void ClientOptions(HttpClient _client)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json")
            );
            _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        }
    }
}