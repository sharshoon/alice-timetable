using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Models
{

    public class TodaySchedule
    {
        public IList<int> weekNumber { get; set; }
        public IList<string> studentGroup { get; set; }
        public IList<string> studentGroupInformation { get; set; }
        public int numSubgroup { get; set; }
        public IList<string> auditory { get; set; }
        public string lessonTime { get; set; }
        public string startLessonTime { get; set; }
        public string endLessonTime { get; set; }
        public string subject { get; set; }
        public string note { get; set; }
        public string lessonType { get; set; }
        public IList<Employee> employee { get; set; }
        public bool zaoch { get; set; }
    }

    public class TomorrowSchedule
    {
        public IList<int> weekNumber { get; set; }
        public IList<string> studentGroup { get; set; }
        public IList<string> studentGroupInformation { get; set; }
        public int numSubgroup { get; set; }
        public IList<string> auditory { get; set; }
        public string lessonTime { get; set; }
        public string startLessonTime { get; set; }
        public string endLessonTime { get; set; }
        public string subject { get; set; }
        public string note { get; set; }
        public string lessonType { get; set; }
        public IList<Employee> employee { get; set; }
        public bool zaoch { get; set; }
    }

    public class TeacherScheduleResponse
    {
        [BsonId]
        public int EmployeeId { get; set; }
        public Employee employee { get; set; }
        public object studentGroup { get; set; }
        public IList<Schedule> schedules { get; set; }
        public IList<ExamSchedule> examSchedules { get; set; }
        public string todayDate { get; set; }
        public IList<TodaySchedule> todaySchedules { get; set; }
        public string tomorrowDate { get; set; }
        public IList<TomorrowSchedule> tomorrowSchedules { get; set; }
        public int currentWeekNumber { get; set; }
    }


}
