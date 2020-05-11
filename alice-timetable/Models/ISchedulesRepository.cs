using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Models
{
    public interface ISchedulesRepository
    {
        static IList<BsuirScheduleResponse> Schedules { get; }
        BsuirScheduleResponse AddSchedule(BsuirScheduleResponse schedule);
        BsuirScheduleResponse DeleteSchedule(int group);
    }
}
