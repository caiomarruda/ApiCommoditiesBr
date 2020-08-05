using System;
using System.Collections.Generic;

namespace ApiCommoditiesBr.Core.Models
{
    public class Products
    {
        public DateTime LastUpdate { get; set; }
        public List<ProductItem> Product { get; set; }
    }

    public class ProductItem
    {
        public string Index { get; set; }
        public string Unit { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
    }
}
