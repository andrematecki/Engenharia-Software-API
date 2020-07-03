using Engenharia_Software.CrossCutting;
using Engenharia_Software.Domain.Entities;
using Engenharia_Software.Domain.Infra.IDP;
using Engenharia_Software.Domain.Infra.Integration;
using Engenharia_Software.Domain.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engenharia_Software.Services
{
    public class SearchService : ISearchService
    {
        private readonly ICallAPI _callAPI;
        

        private string _apiKey;
        private string _context;
        private string _resource;


        public SearchService(ICallAPI callAPI, IConfiguration config)
        {
            _callAPI = callAPI;

            _apiKey = config.GetValue<string>("Google:ApiKey");
            _context = config.GetValue<string>("Google:CustomSearch:Context");
            _resource = config.GetValue<string>("Google:CustomSearch:Resource");

        }

        public ICollection<Search> Search(string q, User user)
        {
            if (string.IsNullOrEmpty(q) || user == null || string.IsNullOrEmpty(user.Username))
                throw new Exception("Parameters 'q' and 'username' are not configured.");

            List<Search> search = new List<Search>();

            var qEnconded = StringUtilities.UrlEncode(q);
            var resource = string.Format(_resource, _apiKey, _context, qEnconded);
            var obj = _callAPI.Get<JObject>(resource);
            
            JToken items = null;
            if (obj != null && (items = obj["items"]) != null)
            {
                foreach (var item in items)
                {
                    var searchItem = new Search
                    {
                        Title = item.Value<string>("title"),
                        Link = item.Value<string>("link"),
                        Snippet = item.Value<string>("snippet")
                    };
                    search.Add(searchItem);
                }
            }
            return search;
        }
    }
}
