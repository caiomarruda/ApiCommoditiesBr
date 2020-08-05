using ApiCommoditiesBr.Core.Models;
using System.Collections.Generic;

namespace ApiCommoditiesBr.Core.Interfaces
{
    public interface ICommodityRepository
    {
        IEnumerable<Product> Get();
    }
}
