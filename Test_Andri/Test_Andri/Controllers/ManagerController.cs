using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Test_Andri.Attributes;
using Test_Andri.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test_Andri.Controllers
{
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        // GET: api/<ManagerController>
        
        [HttpGet]
        public List<dbvaluta> Get()
        {
            var client = new RestClient("https://localhost:44369/api/valuta");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("valuta", "demoasdf1234");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            List<dbvaluta> objResponse = JsonConvert.DeserializeObject<List<dbvaluta>>(response.Content);

            return objResponse;
        }

        // GET api/<ManagerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ManagerController>
        [HttpPost]
        public void Post([FromBody] dbvaluta Request)
        {
            var client = new RestClient("https://localhost:44369/api/valuta");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("valuta", "demoasdf1234");
            request.AddHeader("Content-Type", "application/json");
            var json = JsonConvert.SerializeObject(Request,
                                                Newtonsoft.Json.Formatting.None,
                                                new JsonSerializerSettings
                                                {
                                                    NullValueHandling = NullValueHandling.Ignore
                                                });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        // PUT api/<ManagerController>/5
        [HttpPut]
        public void Put([FromBody] dbvaluta Request)
        {
            var client = new RestClient("https://localhost:44369/api/valuta");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("valuta", "demoasdf1234");
            request.AddHeader("Content-Type", "application/json");
            var json = JsonConvert.SerializeObject(Request,
                                                Newtonsoft.Json.Formatting.None,
                                                new JsonSerializerSettings
                                                {
                                                    NullValueHandling = NullValueHandling.Ignore
                                                });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        // DELETE api/<ManagerController>/5
        [HttpDelete]
        public void Delete([FromBody] dbvaluta Request)
        {
            var client = new RestClient("https://localhost:44369/api/valuta");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("valuta", "demoasdf1234");
            request.AddHeader("Content-Type", "application/json");
            var json = JsonConvert.SerializeObject(Request,
                                                Newtonsoft.Json.Formatting.None,
                                                new JsonSerializerSettings
                                                {
                                                    NullValueHandling = NullValueHandling.Ignore
                                                });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}
