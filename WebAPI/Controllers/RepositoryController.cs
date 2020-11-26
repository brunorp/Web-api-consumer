using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{   
    [ApiController]
    [Route("v1")]
    public class RepositoryController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IFeatureManager _featureManager;
        private readonly IConfiguration _configuration;
        private readonly IRepositoryService _service;
       
        public RepositoryController(IMemoryCache cache, IFeatureManager featureManager, IConfiguration configuration, IRepositoryService service){
            _cache = cache;
            _featureManager = featureManager;
            _configuration = configuration;
            _service = service;
        }

        [HttpGet]
        [Route("{company}")]
        public async Task<List<Repository>> GetRepositoriesCache(string company)
        {
            if (!await _featureManager.IsEnabledAsync(nameof(MyFeatureFlags.CacheRepositories)))
            {
                _cache.Remove(company);
            }
            return await _cache.GetOrCreateAsync(company, entry => 
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_configuration.
                    GetValue<double>("CacheManagement:CacheDuration"));
                return _service.RequestApi(new HttpClient(), company);
            });
        }
    }
}