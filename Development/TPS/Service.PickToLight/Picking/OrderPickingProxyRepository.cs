using Application.Picking.Interface.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.PickToLight.Picking
{
    public interface IOrderPickingProxyRepository
    {
        OrderPickingDto FindOfItem(string itemId);
    }

    public class OrderPickingProxyRepository : IOrderPickingProxyRepository{

        private readonly IHttpClientFactory httpFactory;
        private readonly string picking_api;

        public OrderPickingProxyRepository(
            IHttpClientFactory _httpFactory
            , string pickingApi
            ) {
            httpFactory = _httpFactory;
            picking_api = pickingApi;
        }

        public OrderPickingDto FindOfItem(string itemId) {
            try {
                var request = new HttpRequestMessage(
                HttpMethod.Get
                , $"http://{picking_api}/api/orderpicking?itemid={itemId}");

                request.Headers.Add("Accept", "application/json");

                var client = httpFactory.CreateClient();
                var response = client.Send(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return JsonSerializer
                        .Deserialize<List<OrderPickingDto>>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult())
                        .FirstOrDefault();
                return null;
            } catch (Exception) {
                Debug.WriteLine("GetOrderPicking Confirm error");
                return null;
            }
        }
    }
}
