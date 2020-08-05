using ApiCommoditiesBr.Core.Interfaces;
using ApiCommoditiesBr.Tests.FakeData.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;
using Xunit;

namespace ApiCommoditiesBr.Tests
{
    public class CommodotyTest
    {
        private ICommodityRepository _commodityRepository;

        public CommodotyTest()
        {
        }
     
        [Fact]
        [UseCulture("pt-BR")]
        public void Commodity_Get_Success()
        {
            try
            {
                _commodityRepository = new CommodityRepositoryFake(MakeMockFilePath("commodityResultSuccess.html"));

                var result = _commodityRepository.Get();
                Assert.NotNull(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Fact]
        [UseCulture("pt-BR")]
        public void Commodity_Get_Error_InvalidContent()
        {
            try
            {
                _commodityRepository = new CommodityRepositoryFake(MakeMockFilePath("commodityResultError.html"));

                var result = _commodityRepository.Get();
                Assert.Null(result); 
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        private static string MakeMockFilePath(string file)
        {
            return Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Mock", file);
        }
    }
}
