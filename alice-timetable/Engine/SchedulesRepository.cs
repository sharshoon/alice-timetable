using alice_timetable.Models;
using Alice_Timetable.Engine;
using Alice_Timetable.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
            Schedules = Directory.GetFiles(schedulesPath)
            .Select(file =>
            {
                var content = File.ReadAllText(file);
                var schedule = JsonConvert.DeserializeObject<BsuirScheduleResponse>(content);
                schedule.Group = int.Parse(schedule.studentGroup.name);
                return schedule;
            })
            .ToList();

            TeacherSchedules =
            Directory.GetFiles(teachersSchedulesPath)
            .Select(file =>
            {
                var content = File.ReadAllText(file);
                var schedule = JsonConvert.DeserializeObject<TeacherScheduleResponse>(content);
                return schedule;
            })
            .ToList();

            using var client = new HttpClient();
            CurrentWeek = int.Parse(client.GetStringAsync("http://journal.bsuir.by/api/v1/week").Result);
        }
        private static readonly string schedulesPath = $"{Environment.CurrentDirectory}\\SchedulesRepository";
        private static readonly string teachersSchedulesPath = $"{Environment.CurrentDirectory}\\TeacherSсhedulesRepository";

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

            using var file = new StreamWriter($"{schedulesPath}\\{schedule.Group}.txt", false);
            var content = JsonConvert.SerializeObject(schedule);
            file.Write(content);

            Schedules.Add(schedule);
            return item;
        }

        public BsuirScheduleResponse? DeleteSchedule(int group)
        {
            var item = Schedules.FirstOrDefault(item => item.Group == group);
            if (item != null)
            {
                File.Delete($"{schedulesPath}\\{item.Group}.txt");
                Schedules.Remove(item);
            }
            return item;
        }

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

            using var file = new StreamWriter($"{teachersSchedulesPath}\\{schedule.employee.id}.txt", false);
            var content = JsonConvert.SerializeObject(schedule);
            file.Write(content);

            TeacherSchedules.Add(schedule);
            return item;
        }

        public TeacherScheduleResponse? DeleteTeacherSchedule(int id)
        {
            var item = TeacherSchedules.FirstOrDefault(item => item.employee.id == id);
            if (item != null)
            {
                File.Delete($"{teachersSchedulesPath}\\{item.employee.id}.txt");
                TeacherSchedules.Remove(item);
            }

            return item;
        }
    }
}
