using Alice_Timetable.Models;
using Alice_Timetable.Engine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace Alice_Timetable.Controllers
{
    [ApiController]
    [Route(template: "/")]
    public class MainController : ControllerBase
    {
        private IUsersRepository repository;
        public MainController(IUsersRepository repo)
        {
            repository = repo;
        }
        // Нужно сделать очистку

        private readonly ConcurrentDictionary<string, UserSession> Sessions = new ConcurrentDictionary<string, UserSession>();
        private static readonly JsonSerializerSettings ConverterSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            NullValueHandling = NullValueHandling.Ignore
        };

        public async void Get()
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync("https://journal.bsuir.by/api/v1/studentGroup/schedule?studentGroup=851005");
            var bsuirResponse = JsonConvert.DeserializeObject<BsuirSheduleResponse>(response);
        }

        [HttpPost]
        public Task GetUserRequest()
        {
            using var reader = new StreamReader(Request.Body);
            var body = reader.ReadToEnd();

            var aliceRequest = JsonConvert.DeserializeObject<AliceRequest>(body, ConverterSettings);
            var userId = aliceRequest.Session.UserId;
            var session = Sessions.GetOrAdd(userId, uid => new UserSession(uid, repository));

            var aliceResponse = session.HandleRequest(aliceRequest); 
            var stringResponse = JsonConvert.SerializeObject(aliceResponse, ConverterSettings);
            return Response.WriteAsync(stringResponse);
        }
    }
}
