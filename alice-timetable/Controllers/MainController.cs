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

            //Console.WriteLine($"Объектов в БД, {repository.Users.Count().ToString()}");
            //Console.WriteLine($"Добавлено в список, {States.Count.ToString()}");
        }
        // Нужно сделать очистку

        private static ConcurrentDictionary<string, State> States = new ConcurrentDictionary<string, State>();
        private static readonly JsonSerializerSettings ConverterSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            NullValueHandling = NullValueHandling.Ignore
        };

        public async Task GetSchedule()
        {
            //var client = new HttpClient();
            //var response = await client.GetStringAsync("https://journal.bsuir.by/api/v1/studentGroup/schedule?studentGroup=851005");
            //var bsuirResponse = JsonConvert.DeserializeObject<BsuirScheduleResponse>(response);

        }

        [HttpPost]
        public Task GetUserRequest()
        {
            using var reader = new StreamReader(Request.Body);
            var body = reader.ReadToEnd();
            var aliceRequest = JsonConvert.DeserializeObject<AliceRequest>(body, ConverterSettings);
            var userId = aliceRequest.Session.UserId;
            
            var state = States.GetOrAdd(userId, uid => new State());
            //Console.WriteLine($"Получен State {state.User.Name}, {state.Step.ToString()}");

            var session = new UserSession(userId, repository, state);
            var aliceResponse = session.HandleRequest(aliceRequest, ref state);
            States.AddOrUpdate(userId,p => state, (p,q) => state);

            var stringResponse = JsonConvert.SerializeObject(aliceResponse, ConverterSettings);
            return Response.WriteAsync(stringResponse);
        }
    }
}
