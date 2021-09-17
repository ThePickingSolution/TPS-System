using Business.Domain.Warehouse.Stock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Warehouse.Interface.ItemStocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Warehouse.Controllers
{
    [Route("api/itemstock")]
    [ApiController]
    public class ItemStockController : ControllerBase
    {
        private readonly IItemStockRepository repository;

        public ItemStockController(IItemStockRepository _repository) {
            repository = _repository;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get(string sector) {
            var res = repository.GetItems(sector);
            return Ok(res);
        }
    }
}
