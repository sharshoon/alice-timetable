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
    }
}
