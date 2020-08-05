using ApiCommoditiesBr.Core.Interfaces;
using ApiCommoditiesBr.Core.Models;
using ApiCommoditiesBr.Infrastructure.Repositories;
using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TimeZoneConverter;

namespace ApiCommoditiesBr.Tests.FakeData.Repositories
{
    public class CommodityRepositoryFake : BaseRepository, ICommodityRepository
    {

        private CultureInfo _culture = CultureInfo.GetCultureInfo("pt-BR");
        private static string _filePath;

        public CommodityRepositoryFake(IMemoryCache memoryCache, string filePath) : base(memoryCache)
        {
            _filePath = filePath;
        }
        public Products Get()
        {
            return GetFromSource();
        }

        private Products GetFromSource()
        {
            var htmlWeb = new HtmlWeb();
            var url = htmlWeb.Load(_filePath);

            var lstProducts = new List<ProductItem>();
            Helper.DateTimeHelper.ConvertDateToLocalDateTime(DateTime.Now, out DateTime dateNow);

            var products = new Products
            {
                LastUpdate = dateNow
            };

            var table = url.DocumentNode.SelectSingleNode("//table[@class=\"imagenet-widget-tabela\"]");
            var tableHead = table.SelectNodes("tbody");
            var tableRows = tableHead[0].SelectNodes("tr");

            foreach (var item in tableRows)
            {
                var tdItems = item.SelectNodes($"td");
                Console.WriteLine();
                var spanItems = tdItems[1].SelectNodes("span");

                lstProducts.Add(new ProductItem
                {
                    Index = spanItems[0].InnerText.Trim(),
                    Price = Convert.ToDecimal(tdItems[2].InnerText.Trim().Split(" ")[1], _culture),
                    Date = Convert.ToDateTime(tdItems[0].InnerText.Trim(), _culture),
                    Unit = spanItems[1].InnerText.Trim(),
                    Currency = tdItems[2].InnerText.Trim().Split(" ")[0]
                });
            }

            products.Product = lstProducts;

            return products;
        }
    }
}
