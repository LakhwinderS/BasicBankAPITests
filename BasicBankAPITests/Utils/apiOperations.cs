using BasicBankAPITests.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicBankAPITests.Utils
{
    public class apiOperations
    {
        private RestClient restClient;

        public apiOperations(string uri)
        {
            restClient = new RestClient(uri);
        }
       

        public async Task<RestResponse> DeleteData(string resourcePath)
        {
            var request = new RestRequest(resourcePath, Method.Delete);
            request.AddHeader("Content-Type", "application/json");
            var response = await restClient.ExecuteAsync(request);
            return response;
        }

        public async Task<RestResponse> PostData(string resourcePath, object requestBody)
        {
            var request = new RestRequest(resourcePath, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(requestBody);
            var response = await restClient.ExecuteAsync(request);
            return response;
        }
        public async Task<RestResponse> PutData(string resourcePath, object requestBody)
        {
            var request = new RestRequest(resourcePath, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(requestBody);
            var response = await restClient.ExecuteAsync(request);
            return response;
        }

    }
}
