﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Models
{
    public class User
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public bool DisplaySubjectType { get; set; } = false;
        public bool DisplaySubjectTime { get; set; } = false;
        public bool DisplayEmployeeName { get; set; } = false;
        public bool DisplayAuditory { get; set; } = false;
    }
}
