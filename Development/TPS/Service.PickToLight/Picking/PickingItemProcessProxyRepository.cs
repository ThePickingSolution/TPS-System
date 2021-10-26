using Application.Picking.Interface.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.PickToLight.Picking
{
    public interface IPickingItemProcessProxyRepository
    {
        bool PickItem(PickingItemDto item);
    }

    public class PickingItemProcessProxyRepository : IPickingItemProcessProxyRepository
    {

        private readonly IHttpClientFactory httpFactory;
        private readonly string picking_api;

        public PickingItemProcessProxyRepository(
            IHttpClientFactory _httpFactory
            , string pickingApi
            ) {
            httpFactory = _httpFactory;
            picking_api = pickingApi;
        }

        public bool PickItem(PickingItemDto item) {
            try {
                var request = new HttpRequestMessage(
                HttpMethod.Put
                , $"http://{picking_api}/api/itemprocess/status");

                request.Headers.Add("Accept", "application/json");
                request.Content = JsonContent.Create(item);

                var client = httpFactory.CreateClient();
                var response = client.Send(request);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            } catch (Exception) {
                Debug.WriteLine("UpdateItemStatus Confirm error");
                return false;
            }
        }
    }
}
