using Alice_Timetable.Engine;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Models
{
    public class SchedulesRepository : ISchedulesRepository
    {
        private DatabaseContext context;
        public SchedulesRepository(DatabaseContext ctx)
        {
            Console.WriteLine("Создан объект SchedulesRepository");
            context = ctx;
        }
        public IQueryable<BsuirScheduleResponse> Schedules => context.Schedules;

        public BsuirScheduleResponse TryGetSchedule(int group)
        {
            var schedule = Schedules.FirstOrDefault(schedule => schedule.Id == group);
            if (schedule != null)
            {
                Console.WriteLine($"Получено расписание группы {schedule.Id}");
            }
            else
            {
                Console.WriteLine($"Расписание группы {group} не получено");
            }
            return schedule;
        }

        public BsuirScheduleResponse AddSchedule(BsuirScheduleResponse schedule)
        {
            schedule = context.Add(schedule).Entity;
            Console.WriteLine($"Добавлено новое расписание группы {schedule.Id}");
            return schedule;
        }

        public BsuirScheduleResponse TryDeleteSchedule(int group)
        {
            var schedule = Schedules.FirstOrDefault(schedule => schedule.Id == group);

            if (schedule != null)
            {
                schedule = context.Schedules.Remove(schedule).Entity;
                Console.WriteLine($"Удалено расписание группы {schedule.Id}");
                return schedule;
            }
            else
            {
                return schedule; // null
            }
        }
    }
}
