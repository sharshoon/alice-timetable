using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alice_Timetable.Models;

namespace alice_timetable.Engine
{
    public interface ISchedulesRepository
    {
        IQueryable<BsuirScheduleResponse> Schedules { get; }
        BsuirScheduleResponse TryGetSchedule(int group);
        BsuirScheduleResponse AddSchedule(BsuirScheduleResponse schedule);
        BsuirScheduleResponse TryDeleteSchedule(int group);

    }
}
