using alice_timetable.Models;
using Alice_Timetable.Engine;
using Alice_Timetable.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace alice_timetable.Engine
{
    public class SchedulesRepository : ISchedulesRepository
    {
        public SchedulesRepository(DatabaseContext ctx)
        {
            context = ctx;
            Console.WriteLine("Создан объект SchedulesRepository");

        }
        static SchedulesRepository()
        {

            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var client = storageAccount.CreateCloudFileClient();

            var share = client.GetShareReference("alice-fileshare");
            if (share.ExistsAsync().Result)
            {
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();
                schedulesDir = rootDir.GetDirectoryReference("Schedules");
                teachersSchedulesDir = rootDir.GetDirectoryReference("TeacherSchedules");

                Schedules = schedulesDir.ListFilesAndDirectoriesSegmentedAsync(new FileContinuationToken()).Result.Results
                .Select(
                file =>
                {
                    var fileContent = schedulesDir.GetFileReference(file.Uri.Segments.Last()).DownloadTextAsync().Result;
                    var schedule = JsonConvert.DeserializeObject<BsuirScheduleResponse>(fileContent);
                    schedule.Group = int.Parse(schedule.studentGroup.name);
                    return schedule;
                }).ToList();

                TeacherSchedules = teachersSchedulesDir.ListFilesAndDirectoriesSegmentedAsync(new FileContinuationToken()).Result.Results
                .Select(
                file =>
                {
                    var fileContent = teachersSchedulesDir.GetFileReference(file.Uri.Segments.Last()).DownloadTextAsync().Result;
                    var schedule = JsonConvert.DeserializeObject<TeacherScheduleResponse>(fileContent);
                    return schedule;
                }).ToList();

                
                using var bsuirClient = new HttpClient();
                var response = bsuirClient.GetAsync("http://journal.bsuir.by/api/v1/week").Result;
                if (response.IsSuccessStatusCode)
                {
                    CurrentWeek = int.Parse(response.Content.ReadAsStringAsync().Result);

                    rootDir.GetFileReference("currentweek.txt").UploadTextAsync($"{CurrentWeek.ToString()}");
                }
                else
                {
                    var CurrentWeek = int.Parse(rootDir.GetFileReference("currentweek.txt").DownloadTextAsync().Result);
                }
            }
            else
            {
                throw new Exception("Ошибка, такого файлового хранилища нет!");
            }

        }
        private static readonly string connectionString = $"DefaultEndpointsProtocol=https;AccountName=csb10032000bf1a412a;AccountKey=/PyUURwZBhxZqjFNaRLwA1KcI7YdoH6UaPB+K6OJNvOpJ82iNoUOvgIO0ogpgkBouJq/SgVXwO+Qbh/FBaNV6g==;EndpointSuffix=core.windows.net";
        private static readonly CloudFileDirectory schedulesDir;
        private static readonly CloudFileDirectory teachersSchedulesDir;


        private DatabaseContext context;
        public IQueryable<Teacher> Teachers => context.Teachers;
        public static IList<BsuirScheduleResponse> Schedules;
        public static IList<TeacherScheduleResponse> TeacherSchedules;
        public static int CurrentWeek;
        public BsuirScheduleResponse AddSchedule(BsuirScheduleResponse schedule)
        {
            var item = Schedules.FirstOrDefault(item => item.Group == schedule.Group);
            if (item != null)
            {
                Schedules.Remove(item);
            }

            schedulesDir.GetFileReference($"{ schedule.Group}.txt").UploadTextAsync(JsonConvert.SerializeObject(schedule));
            Schedules.Add(schedule);
            return item;
        }

        public BsuirScheduleResponse? DeleteSchedule(int group)
        {
            var item = Schedules.FirstOrDefault(item => item.Group == group);
            if (item != null)
            {
                schedulesDir.GetFileReference($"{item.Group}.txt").DeleteIfExistsAsync();
            }
            return item;
        }

        // Для заполнения базы данных один раз
        private void GetTeachers()
        {
            using var client = new HttpClient();
            var response = client.GetStringAsync("https://journal.bsuir.by/api/v1/employees").Result;

            var employees = JsonConvert.DeserializeObject<List<Teacher>>(response);

            employees.ForEach(empl => context.Teachers.Add(empl));
            context.SaveChanges();
        }

        public TeacherScheduleResponse AddTeacherSchedule(TeacherScheduleResponse schedule)
        {
            var item = TeacherSchedules.FirstOrDefault(item => item.employee.id == schedule.employee.id);
            if (item != null)
            {
                TeacherSchedules.Remove(item);
            }

            teachersSchedulesDir.GetFileReference($"{schedule.employee.id}.txt").UploadTextAsync(JsonConvert.SerializeObject(schedule));
            TeacherSchedules.Add(schedule);
            return item;
        }

        public TeacherScheduleResponse? DeleteTeacherSchedule(int id)
        {
            var item = TeacherSchedules.FirstOrDefault(item => item.employee.id == id);
            if (item != null)
            {
                schedulesDir.GetFileReference($"{item.employee.id}.txt").DeleteIfExistsAsync();
            }
            return item;
        }
    }
}
