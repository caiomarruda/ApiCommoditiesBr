using ApiCommoditiesBr.Core.Interfaces;
using ApiCommoditiesBr.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiCommoditiesBr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommoditiesController : ControllerBase
    {
        private readonly ICommodityRepository _commodityRepository;
        public CommoditiesController(ICommodityRepository commodityRepository)
        {
            _commodityRepository = commodityRepository;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            try
            {
                return _commodityRepository.Get();
            }
            catch (System.Exception ex)
            {
                BadRequest();
            }

            return null;
        }
    }
}
