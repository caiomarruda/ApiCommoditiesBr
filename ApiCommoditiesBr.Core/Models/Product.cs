using System;

namespace ApiCommoditiesBr.Core.Models
{
    public class Product
    {
        public string Index { get; set; }
        public string Unit { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
    }
}
