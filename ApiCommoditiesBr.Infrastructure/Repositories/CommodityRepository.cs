using ApiCommoditiesBr.Core.Interfaces;
using ApiCommoditiesBr.Core.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ApiCommoditiesBr.Infrastructure.Repositories
{
    public class CommodityRepository : ICommodityRepository
    {
        private CultureInfo _culture = CultureInfo.GetCultureInfo("pt-BR");
        private readonly IConfiguration _configuration;

        public CommodityRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Product> Get()
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
                    Index = spanItems[0].InnerText,
                    Price = Convert.ToDecimal(tdItems[2].InnerText.Split(" ")[1], _culture),
                    Date = Convert.ToDateTime(tdItems[0].InnerText, _culture),
                    Unit = spanItems[1].InnerText,
                    Currency = tdItems[2].InnerText.Split(" ")[0]
                });
            }

            return lstProducts;
        }
    }
}
