using Alice_Timetable.Engine;
using Alice_Timetable.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Models
{
    public class SchedulesRepository : ISchedulesRepository
    {
        private static string repositoryPath = $"{Environment.CurrentDirectory}\\SchedulesRepository";
        public SchedulesRepository()
        {
            Console.WriteLine("Создан объект SchedulesRepository");
        }

        static SchedulesRepository()
        {
            Schedules = new List<BsuirScheduleResponse>();
            var files = Directory.GetFiles(repositoryPath).ToList();
            files.ForEach(
            file =>
            {
                var content = File.ReadAllText(file);
                var schedule = JsonConvert.DeserializeObject<BsuirScheduleResponse>(content);
                schedule.Group = int.Parse(schedule.studentGroup.name);
                Schedules.Add(schedule);
            });
        }
        public static IList<BsuirScheduleResponse> Schedules;

        public BsuirScheduleResponse AddSchedule(BsuirScheduleResponse schedule)
        {
            var item = Schedules.FirstOrDefault(item => item.Group == schedule.Group);
            if (item != null)
            {
                Schedules.Remove(item);
            }

            using var file = new StreamWriter($"{repositoryPath}\\{schedule.Group}.txt", false);
            var content = JsonConvert.SerializeObject(schedule);
            file.Write(content);

            Schedules.Add(schedule);
            return item;
        }

        public BsuirScheduleResponse DeleteSchedule(int group)
        {
            var item = Schedules.FirstOrDefault(item => item.Group == group);
            if (item != null)
            {
                File.Delete($"{repositoryPath}\\{item.Group}.txt");
                Schedules.Remove(item);
            }

            return item;
        }

    }
}
