using ApiCommoditiesBr.Core.Interfaces;
using ApiCommoditiesBr.Core.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace ApiCommoditiesBr.Core.Services
{
    public class CommodityService : ICommodityService
    {
        public Products GetFromSource(string filePath)
        {
            var htmlWeb = new HtmlWeb();
            var url = htmlWeb.Load(filePath);

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
                    Price = Convert.ToDecimal(tdItems[2].InnerText.Trim().Split(" ")[1]),
                    Date = Convert.ToDateTime(tdItems[0].InnerText.Trim()),
                    Unit = spanItems[1].InnerText.Trim(),
                    Currency = tdItems[2].InnerText.Trim().Split(" ")[0]
                });
            }

            products.Product = lstProducts;

            return products;
        }
    }
}
