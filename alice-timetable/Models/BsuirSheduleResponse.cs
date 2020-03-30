using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Models
{
    public class StudentGroup
    {
        public string name { get; set; }
        public int facultyId { get; set; }
        public object facultyName { get; set; }
        public int specialityDepartmentEducationFormId { get; set; }
        public object specialityName { get; set; }
        public int course { get; set; }
        public int id { get; set; }
        public string calendarId { get; set; }
    }

    public class Employee
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public object rank { get; set; }
        public string photoLink { get; set; }
        public string calendarId { get; set; }
        public IList<string> academicDepartment { get; set; }
        public int id { get; set; }
        public string fio { get; set; }
    }

    public class Schedule
    {
        public string weekDay { get; set; }
        public IList<Schedules.Schedule> schedule { get; set; }
    }

    public class ExamSchedule
    {
        public string weekDay { get; set; }
        public IList<Schedules.Schedule> schedule { get; set; }
    }

    public class BsuirSheduleResponse
    {
        public object employee { get; set; }
        public StudentGroup studentGroup { get; set; }
        public IList<Schedule> schedules { get; set; }
        public IList<ExamSchedule> examSchedules { get; set; }
        public string todayDate { get; set; }
        public IList<object> todaySchedules { get; set; }
        public string tomorrowDate { get; set; }
        public IList<object> tomorrowSchedules { get; set; }
        public int currentWeekNumber { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
        public object sessionStart { get; set; }
        public object sessionEnd { get; set; }
    }
}

namespace Alice_Timetable.Models.Schedules
{
    public class Schedule
    {
        public IList<int> weekNumber { get; set; }
        public IList<string> studentGroup { get; set; }
        public IList<string> studentGroupInformation { get; set; }
        public int numSubgroup { get; set; }
        public IList<string> auditory { get; set; }
        public string lessonTime { get; set; }
        public string startLessonTime { get; set; }
        public string endLessonTime { get; set; }
        public object gradebookLesson { get; set; }
        public string subject { get; set; }
        public object note { get; set; }
        public string lessonType { get; set; }
        public IList<Employee> employee { get; set; }
        public object studentGroupModelList { get; set; }
        public bool zaoch { get; set; }
        public object gradebookLessonlist { get; set; }
    }
}