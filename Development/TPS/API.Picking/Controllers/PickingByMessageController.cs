using Application.Picking.Interface.PickingItems;
using Business.Domain.Picking;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Picking.Interface.OrderPickings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Picking.Controllers
{
    [Route("api/bymessage")]
    [ApiController]
    public class PickingByMessageController : ControllerBase
    {
        private readonly IPickingItemProcessApplication itemProcess;
        private readonly IOrderPickingQuery orderPickingQuery;

        public PickingByMessageController(IPickingItemProcessApplication _itemProcess
            , IOrderPickingQuery _orderPickingQuery) {
            itemProcess = _itemProcess;
            orderPickingQuery = _orderPickingQuery;
        }

        [HttpPost]
        [Route("")]
        public IActionResult Pick(string payload) {
            var payloadParts = payload.Split(';').Select(x => x.Trim()).ToArray();

            string itemid = payloadParts[0];
            int qty = Int32.Parse(payloadParts[1]);

            var orderpicking = orderPickingQuery.New()
            .ContainsItem(itemid)
            .FirstOrDefault();

            if (orderpicking.Status == PickingStatus.WIP) {
                var item = orderpicking.Items.First(f => f.Id == itemid);

                var sameSkuItems = orderpicking.Items.Where(w => w.SKU == item.SKU);
                var count = qty;

                foreach (var i in sameSkuItems) {
                    itemProcess.SetItemStatus(i.Id, --count < 0 ? ItemStatus.MISSING : ItemStatus.PICKED, orderpicking.Operator.Id.ToString());
                }
            }


            return Ok();
        }
    }
}
