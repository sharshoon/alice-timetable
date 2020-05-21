using Alice_Timetable.Models;
using Alice_Timetable.Engine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Threading.Tasks;
using alice_timetable.Engine;
using Microsoft.WindowsAzure.Storage;
using System.Net.Http;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Alice_Timetable.Controllers
{
    [ApiController]
    [Route(template: "/")]
    public class MainController : ControllerBase
    {
        private IUsersRepository usersRepository;
        private ISchedulesRepository schedulesRepository;
        public MainController(IUsersRepository usersRepo, ISchedulesRepository schedulesRepo)
        {
            usersRepository = usersRepo;
            schedulesRepository = schedulesRepo;

            //using var bsuir = new HttpClient();
            //var response = bsuir.GetStringAsync("https://journal.bsuir.by/api/v1/studentGroup/schedule?studentGroup=851005").Result;
            //var bsonResponse = BsonSerializer.Deserialize<BsonDocument>(response);
           
            //string connectionString = "mongodb://localhost";
            //var client = new MongoClient(connectionString);
            //var database = client.GetDatabase("test");
            //var collection = database.GetCollection<BsonDocument>("people");
            //collection.InsertOneAsync(bsonResponse).RunSynchronously();
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

        public string Get()
        {
            return "It works!";
        }

        [HttpPost]
        public Task GetUserRequest()
        {
            using var reader = new StreamReader(Request.Body);
            var body = reader.ReadToEndAsync().Result;

            var aliceRequest = JsonConvert.DeserializeObject<AliceRequest>(body, ConverterSettings);
            var userId = aliceRequest.Session.UserId;

            // Создаем новую сессию и передаем туда уже имеющуюся/новую информацию
            // о шаге пользователя и его личных данных
            var session = new UserSession(userId, usersRepository, schedulesRepository, State);
            //Выполняем запрос и сохраняем в State обновленные данные и пользователе
            var aliceResponse = session.HandleRequest(aliceRequest, ref State);

            var stringResponse = JsonConvert.SerializeObject(aliceResponse, ConverterSettings);

            return Response.WriteAsync(stringResponse);
        }
    }
}
