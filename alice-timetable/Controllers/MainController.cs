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

        private static State State = new State();
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

            // Создаем новую сессию и передаем туда уже имеющуюся/новую информацию
            // о шаге пользователя и его личных данных
            var session = new UserSession(userId, repository, State);
            // Выполняем запрос и сохраняем в State обновленные данные и пользователе
            var aliceResponse = session.HandleRequest(aliceRequest, ref State);

            var stringResponse = JsonConvert.SerializeObject(aliceResponse, ConverterSettings);
            return Response.WriteAsync(stringResponse);
        }
    }
}
