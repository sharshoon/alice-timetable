using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string rank { get; set; }
        public string photoLink { get; set; }
        public string calendarId { get; set; }
        public IList<string> academicDepartment { get; set; }
        public int id { get; set; }
        public string fio { get; set; }
    }
}
