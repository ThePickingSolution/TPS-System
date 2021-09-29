using Business.Domain.Warehouse.Stock;
using Service.PickToLight.Interface.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.PickToLight.Warehouse
{
    public class ItemStockProxyRepository : IItemStockProxyRepository
    {
        private readonly IHttpClientFactory factory;
        private readonly string warehouse_api;
        public ItemStockProxyRepository(string warehouseApi, IHttpClientFactory _factory) {
            warehouse_api = warehouseApi;
            factory = _factory;
        }

        public List<ItemStock> Get(string sector) {
            List<ItemStock> stock = null;

            var request = new HttpRequestMessage(
                HttpMethod.Get
                , $"http://{warehouse_api}/api/itemstock?sector={sector}");

            request.Headers.Add("Accept", "application/json");

            var client = factory.CreateClient();
            var response = client.Send(request);
            if (response.IsSuccessStatusCode) {
                string json = response.Content.ReadAsStringAsync().Result;
                stock = JsonSerializer.Deserialize<List<ItemStock>>(json);
            }


            return stock ?? new List<ItemStock>();
        }
    }
}
