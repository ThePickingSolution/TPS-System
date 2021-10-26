using Application.Picking.Interface.DTOs;
using Application.Picking.Interface.PickingItems;
using Business.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Picking.Controllers
{
    [Route("api/itemprocess")]
    [ApiController]
    public class PickingItemProcessController : ControllerBase
    {
        private readonly IPickingItemProcessApplication itemProcess;

        public PickingItemProcessController(IPickingItemProcessApplication _itemProcess) {
            itemProcess = _itemProcess;
        }


        [HttpPut]
        [Route("status")]
        public IActionResult SetStatus(PickingItemDto item) {
            try {
                itemProcess.SetItemStatus(item.Id, item.Status, item.Operator);
                return Ok();
            } catch (DomainException ex) {
                return BadRequest(ex.Message);
            } 
        }
    }
}
