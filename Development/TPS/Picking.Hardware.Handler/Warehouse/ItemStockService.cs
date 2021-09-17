using Business.Domain.Warehouse.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Picking.Hardware.Handler.Warehouse
{
    public class ItemStockService
    {
        private readonly IHttpClientFactory factory;
        private readonly string warehouse_api;
        public ItemStockService(string warehouseApi, IHttpClientFactory _factory) {
            warehouse_api = warehouseApi;
            factory = _factory;
        }

        public ItemStock Get(string sector, string sku) {
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


            stock = stock ?? new List<ItemStock>();

            return stock.FirstOrDefault(f => f.SKU == sku);
        }
    }
}
