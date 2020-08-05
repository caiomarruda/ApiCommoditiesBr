﻿using ApiCommoditiesBr.Core.Interfaces;
using ApiCommoditiesBr.Core.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ApiCommoditiesBr.Infrastructure.Repositories
{
    public class CommodityRepository : BaseRepository, ICommodityRepository
    {
        private static readonly string cacheIndexName = "commodities";
        private readonly IConfiguration _configuration;
        
        private static string _filePath;
        private static bool _cacheEnabled;
        private static int _cacheTtl;

        public CommodityRepository(IConfiguration configuration, IMemoryCache memoryCache) : base(memoryCache)
        {
            _configuration = configuration;
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
                    return SetInMemoryCache(GetFromSource(), cacheIndexName, _cacheTtl);
            }
            
            return retProducts ?? GetFromSource();
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
