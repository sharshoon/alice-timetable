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
        static int CurrentWeek { get; }
        IQueryable<Teacher> Teachers { get; }
        Task<BsuirScheduleResponse> GetSchedule(int group);
        Task<TeacherScheduleResponse> GetTeacherSchedule(int id);
        void AddSchedule(BsuirScheduleResponse schedule);
        void DeleteSchedule(int group);
        void AddTeacherSchedule(TeacherScheduleResponse schedule);
        void DeleteTeacherSchedule(int id);
        void UpdateSchedule(int group, BsuirScheduleResponse newSchedule);
        void UpdateTeacherSchedule(int id, TeacherScheduleResponse TeacherScheduleResponse);
    }
}
