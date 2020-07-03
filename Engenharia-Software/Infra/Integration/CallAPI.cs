using Engenharia_Software.Domain.CrossCutting;
using Engenharia_Software.Domain.Infra.Integration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Engenharia_Software.Infra.Integration
{
    public class CallAPI : ICallAPI
    {
        private string _baseUrl;
        private readonly IJsonConverter _jsonConverter;
        private HttpClient _client;

        public CallAPI(IJsonConverter jsonConverter)
        {
            _jsonConverter = jsonConverter;
            _client = new HttpClient();            
        }

        public T Get<T>(string resource)
        {
            HttpResponseMessage response = _client.GetAsync(resource).Result;
            return GetResponse<T>(response);
        }

        private T GetResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode || response.Content == null)
                throw new Exception("Error to call: " + response.RequestMessage.RequestUri.AbsolutePath + " | Response Code: " + response.StatusCode);
        
            string result = response.Content.ReadAsStringAsync().Result;
                        
            if (result == null)
                return default(T);
            
            T obj = _jsonConverter.DeserializeObject<T>(result);
            return obj;
        }
    }
}
