using Application.Picking.Interface.DTOs;
using Application.Picking.Interface.OrderPickings;
using Business.Domain.Exceptions;
using Infrastructure.String;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Picking.Controllers
{
    [Route("api/process")]
    [ApiController]
    public class OrderPickingProcessController : ControllerBase
    {
        private readonly IOrderPickingProcessApplication app;

        public OrderPickingProcessController(IOrderPickingProcessApplication _app) {
            app = _app;
        }

        [HttpGet]
        [Route("next")]
        public IActionResult NextOrderPicking(string sector) {
            var id = app.Next(sector);
            return !id.IsNullOrEmpty() ? Ok(id) : NoContent();
        }

        [HttpPost]
        [Route("start")]
        public IActionResult Start(OrderPickingDto picking) {
            try {
                return Ok(app.Start(picking.Id, picking.Sector, picking.Operator));
            } catch (DomainException ex) {
                return BadRequest(ex.Message);
            } catch(Exception ex) {
                StringBuilder message = new StringBuilder();

                Exception aux = ex;
                do {
                    message.AppendLine(aux.Message);
                    message.AppendLine(aux.StackTrace);
                    message.AppendLine("______________________");
                    aux = aux.InnerException;
                } while (aux != null);
                return BadRequest(message.ToString());
            }
        }
    }
}
