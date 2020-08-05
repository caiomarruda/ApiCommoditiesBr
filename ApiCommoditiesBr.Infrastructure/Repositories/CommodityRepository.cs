using ApiCommoditiesBr.Core.Interfaces;
using ApiCommoditiesBr.Core.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace ApiCommoditiesBr.Infrastructure.Repositories
{
    public class CommodityRepository : BaseRepository, ICommodityRepository
    {
        private CultureInfo _culture = CultureInfo.GetCultureInfo("pt-BR");
        private static readonly string cacheIndexName = "commodities";
        private readonly IConfiguration _configuration;

        public CommodityRepository(IConfiguration configuration, IMemoryCache memoryCache) : base(memoryCache)
        {
            _configuration = configuration;
        }

        public IEnumerable<Product> Get()
        {
            var retValues = GetInMemoryCache<List<Product>>(cacheIndexName);

            if (retValues == null)
                return SetInMemoryCache(GetFromSource(), cacheIndexName);

            return retValues;
        }

        private List<Product> GetFromSource()
        {
            var htmlDocument = new HtmlWeb();
            var url = htmlDocument.Load(_configuration["CommoditiesUrl"]);

            var lstProducts = new List<Product>();
            var table = url.DocumentNode.SelectSingleNode("//table[@class=\"imagenet-widget-tabela\"]");
            var tableHead = table.SelectNodes("tbody");
            var tableRows = tableHead[0].SelectNodes("tr");

            foreach (var item in tableRows)
            {
                var tdItems = item.SelectNodes($"td");
                Console.WriteLine();
                var spanItems = tdItems[1].SelectNodes("span");

                lstProducts.Add(new Product
                {
                    Index = spanItems[0].InnerText.Trim(),
                    Price = Convert.ToDecimal(tdItems[2].InnerText.Trim().Split(" ")[1], _culture),
                    Date = Convert.ToDateTime(tdItems[0].InnerText.Trim(), _culture),
                    Unit = spanItems[1].InnerText.Trim(),
                    Currency = tdItems[2].InnerText.Trim().Split(" ")[0]
                });
            }

            return lstProducts;
        }
    }
}
