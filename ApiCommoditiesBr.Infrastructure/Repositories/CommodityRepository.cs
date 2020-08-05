using ApiCommoditiesBr.Core.Interfaces;
using ApiCommoditiesBr.Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;

namespace ApiCommoditiesBr.Infrastructure.Repositories
{
    public class CommodityRepository : BaseRepository, ICommodityRepository
    {
        private static readonly string cacheIndexName = "commodities";
        private readonly IConfiguration _configuration;
        private readonly ICommodityService _commodityService;

        private static string _filePath;
        private static bool _cacheEnabled;
        private static int _cacheTtl;

        public CommodityRepository(IConfiguration configuration, IMemoryCache memoryCache, ICommodityService commodityService) : base(memoryCache)
        {
            _configuration = configuration;
            _commodityService = commodityService;

            _filePath = _configuration["CommoditiesUrl"];
            _cacheEnabled = Convert.ToBoolean(_configuration["EnableCache"]);
            _cacheTtl = Convert.ToInt32(_configuration["TtlCache"]);
        }

        public Products Get()
        {
            var retProducts = new Products();

            if (_cacheEnabled)
            {
                retProducts = GetInMemoryCache<Products>(cacheIndexName);

                if (retProducts == null)
                    return SetInMemoryCache(_commodityService.GetFromSource(_filePath), cacheIndexName, _cacheTtl);
            }
            
            return retProducts ?? _commodityService.GetFromSource(_filePath);
        }

        
    }
}
