using Alice_Timetable.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Controllers
{
    [ApiController]
    [Route(template: "/")]
    public class MainController : ControllerBase
    {
        private static readonly JsonSerializerSettings ConverterSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            NullValueHandling = NullValueHandling.Ignore
        };

        [HttpGet]
        public string Get()
        {
            return "Test";
        }

        [HttpPost]
        public Task GetUserRequest()
        {
            using var reader = new StreamReader(Request.Body);
            var body = reader.ReadToEnd();

            var aliceRequest = JsonConvert.DeserializeObject<AliceRequest>(body, ConverterSettings);
            var aliceResponse = new AliceResponse(aliceRequest)
            {
                Response = { Text = "Привет!!!" }
            };

            using (var sw = new StreamWriter("log.txt", true))
            {
                sw.Write("ЗАПИСАЛ");
            }

            var stringResponse = JsonConvert.SerializeObject(aliceResponse, ConverterSettings);
            return Response.WriteAsync(stringResponse);
        }
    }
}
