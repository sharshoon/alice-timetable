using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Models
{
    public class GroupInfo
    {
        public string name { get; set; }
        public int facultyId { get; set; }
        public int specialityDepartmentEducationFormId { get; set; }
        public int? course { get; set; }
        public int? id { get; set; }
        public string calendarId { get; set; }
    }
}
