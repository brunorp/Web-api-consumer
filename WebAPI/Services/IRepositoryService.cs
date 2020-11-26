using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IRepositoryService
    {
        Task<List<Repository>> RequestApi(HttpClient _client, string company);
    }
}