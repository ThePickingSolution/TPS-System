using Application.Picking.Interface.DTOs;
using Application.Picking.Interface.DTOs.Params;
using Application.Picking.Interface.OrderPickings;
using Business.Domain.Picking;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Picking.Controllers
{
    [Route("api/orderpicking")]
    [ApiController]
    public class OrderPickingController : ControllerBase
    {

        private readonly IOrderPickingApplication app;

        public OrderPickingController(IOrderPickingApplication _app) {
            app = _app;
        }


        [HttpGet]
        public IEnumerable<OrderPickingDto> Get(string id=null,string sector = null, string op = null, string container = null, PickingStatus? status = null)
        {
            var _params = new OrderPickingParams();
            _params.Limit = 50;
            if (id != null) _params.SetIdFilter(id);
            if (sector != null) _params.SetSectorFilter(sector);
            if (op != null) _params.SetOperatorFilter(op);
            if (container != null) _params.SetContainerFilter(container);
            if (status != null) _params.SetStatusFilter(status.Value);

            return app.Get(_params);
        }
    }
}
