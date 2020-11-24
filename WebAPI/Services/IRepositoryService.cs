using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IRepositoryService
    {
        Task<List<Repository>> RequestApi(string company);
    }
}