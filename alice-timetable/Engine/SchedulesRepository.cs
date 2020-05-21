using alice_timetable.Engine;
using alice_timetable.Models;
using Alice_Timetable.Engine;
using Alice_Timetable.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace alice_timetable.Engine
{
    public class SchedulesRepository : ISchedulesRepository
    {
        public SchedulesRepository(DatabaseContext dbCtx, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SchedulesConnection");
            MongoClient client = new MongoClient(connectionString);
            var connection = new MongoUrlBuilder(connectionString);
            database = client.GetDatabase(connection.DatabaseName);

            context = dbCtx;


        }
        static SchedulesRepository()
        {
            using var bsuirClient = new HttpClient();
            var response = bsuirClient.GetAsync("http://journal.bsuir.by/api/v1/week").Result;
            if (response.IsSuccessStatusCode)
            {
                CurrentWeek = int.Parse(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                throw new Exception("Сервер не отвечает");
            }
        }

        private IMongoDatabase database;
        public static int CurrentWeek;
        private DatabaseContext context;
        public IQueryable<Teacher> Teachers => context.Teachers;

        public IMongoCollection<BsuirScheduleResponse> Schedules
        {
            get { return database.GetCollection<BsuirScheduleResponse>("Schedules"); }
        }

        public IMongoCollection<TeacherScheduleResponse> TeacherSchedules
        {
            get { return database.GetCollection<TeacherScheduleResponse>("TeacherSchedules"); }
        }

        public async Task<BsuirScheduleResponse> GetSchedule(int group)
        {
            return await Schedules.Find(new BsonDocument("Group", group)).FirstOrDefaultAsync();
        }

        public async Task<TeacherScheduleResponse> GetTeacherSchedule(int id)
        {
            return await TeacherSchedules.Find(new BsonDocument("employee.id", id)).FirstOrDefaultAsync();
        }

        public async void AddSchedule(BsuirScheduleResponse schedule)
        {
            await Schedules.InsertOneAsync(schedule);
        }

        public async void AddTeacherSchedule(TeacherScheduleResponse schedule)
        {
            await TeacherSchedules.InsertOneAsync(schedule);
        }

        public async void DeleteSchedule(int group)
        {
            var filter = Builders<BsuirScheduleResponse>.Filter.Eq("Group", group);
            await Schedules.DeleteOneAsync(filter);
        }

        public async void DeleteTeacherSchedule(int id)
        {
            var filter = Builders<TeacherScheduleResponse>.Filter.Eq("Id", id);
            await TeacherSchedules.DeleteOneAsync(filter);
        }
    }
}
