using alice_timetable.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Models
{
    public class StudentGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int studentGroupId { get; set; }
        public string name { get; set; }
        public int facultyId { get; set; }
        public string facultyName { get; set; }
        public int specialityDepartmentEducationFormId { get; set; }
        public string specialityName { get; set; }
        public int course { get; set; }
        public int id { get; set; }
        public string calendarId { get; set; }
    }

    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeId { get; set; }
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string rank { get; set; }
        public string photoLink { get; set; }
        public string calendarId { get; set; }
        public string[] academicDepartment { get; set; }
        public string fio { get; set; }
    }

    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string weekDay { get; set; }
        public IList<Schedules.Schedule> schedule { get; set; }
    }

    public class ExamSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string weekDay { get; set; }
        public IList<Schedules.Schedule> schedule { get; set; }
    }

    //public class BsuirScheduleResponse
    //{
    //    public object employee { get; set; }
    //    public StudentGroup studentGroup { get; set; }
    //    public IList<Schedule> schedules { get; set; }
    //    public IList<ExamSchedule> examSchedules { get; set; }
    //    public string todayDate { get; set; }
    //    public IList<object> todaySchedules { get; set; }
    //    public string tomorrowDate { get; set; }
    //    public IList<object> tomorrowSchedules { get; set; }
    //    public int currentWeekNumber { get; set; }
    //    public string dateStart { get; set; }
    //    public string dateEnd { get; set; }
    //    public object sessionStart { get; set; }
    //    public object sessionEnd { get; set; }
    //}

    public class BsuirScheduleResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Group { get; set; }
        public string employee { get; set; }
        public StudentGroup studentGroup { get; set; }
        public IList<Alice_Timetable.Models.Schedule> schedules { get; set; }
        public IList<ExamSchedule> examSchedules { get; set; }
        public string todayDate { get; set; }
        public IList<Alice_Timetable.Models.Schedules.Schedule> todaySchedules { get; set; }
        public string tomorrowDate { get; set; }
        public IList<Alice_Timetable.Models.Schedules.Schedule> tomorrowSchedules { get; set; }
        public int currentWeekNumber { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
        public string sessionStart { get; set; }
        public string sessionEnd { get; set; }
    }
}

namespace Alice_Timetable.Models.Schedules
{
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int[] weekNumber { get; set; }
        public string[] studentGroup { get; set; }
        public string[] studentGroupInformation { get; set; }
        public int numSubgroup { get; set; }
        public string[] auditory { get; set; }
        public string lessonTime { get; set; }
        public string startLessonTime { get; set; }
        public string endLessonTime { get; set; }
        public string gradebookLesson { get; set; }
        public string subject { get; set; }
        public string note { get; set; }
        public string lessonType { get; set; }
        public IList<Employee> employee { get; set; }
        public string[] studentGroupModelList { get; set; }
        public bool zaoch { get; set; }
        public string[] gradebookLessonlist { get; set; }
    }
}