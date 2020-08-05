using ApiCommoditiesBr.Core.Interfaces;
using ApiCommoditiesBr.Core.Models;
using ApiCommoditiesBr.Core.Services;

namespace ApiCommoditiesBr.Tests.FakeData.Repositories
{
    public class CommodityRepositoryFake : ICommodityRepository
    {
        private static string _filePath;
        private static ICommodityService _commodityService;

        public CommodityRepositoryFake(string filePath)
        {
            _filePath = filePath;
            _commodityService = new CommodityService();
        }
        public Products Get()
        {
            return GetFromSource();
        }

        private Products GetFromSource()
        {
            return _commodityService.GetFromSource(_filePath);
        }
    }
}
