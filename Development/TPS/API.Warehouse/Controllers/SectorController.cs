using Business.Domain.Warehouse.Stock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Warehouse.Interface.Sectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Warehouse.Controllers
{
    [Route("api/sector")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly ISectorRepository repository;

        public SectorController(ISectorRepository _repository) {
            repository = _repository;
        }

        [HttpGet]
        public IActionResult Get() {
            IEnumerable<Sector> sector = repository.Get();
            return Ok(sector);
        }
    }
}
