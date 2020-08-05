using ApiCommoditiesBr.Core.Models;

namespace ApiCommoditiesBr.Core.Interfaces
{
    public interface ICommodityService
    {
        Products GetFromSource(string filePath);
    }
}
