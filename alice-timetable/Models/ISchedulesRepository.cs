using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Models
{
    public interface ISchedulesRepository
    {
        IQueryable<BsuirScheduleResponse> Schedules { get; }
        BsuirScheduleResponse TryGetSchedule(int group);
        BsuirScheduleResponse AddSchedule(BsuirScheduleResponse schedule);
        BsuirScheduleResponse TryDeleteSchedule(int group);
    }
}
