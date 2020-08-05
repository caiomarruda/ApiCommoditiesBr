using ApiCommoditiesBr.Core.Interfaces;
using ApiCommoditiesBr.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(new Notification
                {
                    Success = true,
                    Data = _commodityRepository.Get()
            });

            }
            catch (Exception ex)
            {
                return BadRequest(new Notification
                {
                    Success = false,
                    Errors = ex.Message
                });
            }
        }
    }
}
