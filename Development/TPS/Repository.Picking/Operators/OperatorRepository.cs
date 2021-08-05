using Business.Domain.People;
using Infrastructure.String;
using Repository.Picking.Interface.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.Picking.Operators
{
    public class OperatorRepository : IOperatorRepository
    {
        private readonly IHttpClientFactory factory;
        private readonly string adm_api;
        public OperatorRepository(string admApi, IHttpClientFactory _factory) {
            adm_api = admApi;
            factory = _factory;
        }

        public Operator Get(string id) {
            Operator op = null;
            if (id.IsNullOrEmpty())
                return op;

            var gid = new Guid(id);

            var request = new HttpRequestMessage(
                HttpMethod.Get
                ,$"http://{adm_api}/api/user?id={gid.ToString()}");

            request.Headers.Add("Accept", "application/json");

            var client = factory.CreateClient();
            var response = client.Send(request);
            if (response.IsSuccessStatusCode) {
                var dto = JsonSerializer.Deserialize<OperatorHttpDto>(
                    response.Content
                    .ReadAsStringAsync()
                    .Result);
                if(dto != null) {
                    op = new Operator(dto.Id, dto.Username, dto.Name);
                }
            }


            return op;
        }
    }
}
