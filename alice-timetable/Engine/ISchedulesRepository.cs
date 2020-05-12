using alice_timetable.Models;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Engine
{
    public interface ISchedulesRepository
    {
        static IList<BsuirScheduleResponse> Schedules { get; }
        static IList<TeacherScheduleResponse> TeacherSchedules { get; }
        static int CurrentWeek { get; }
        IQueryable<Teacher> Teachers { get; }
        BsuirScheduleResponse AddSchedule(BsuirScheduleResponse schedule);
        BsuirScheduleResponse DeleteSchedule(int group);
        TeacherScheduleResponse AddTeacherSchedule(TeacherScheduleResponse schedule);
        TeacherScheduleResponse DeleteTeacherSchedule(int id);
    }
}
