﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Models
{
    public class User
    {
        [Key]
        public string ID { get; set; }
        public List<WebHook> WebHooks { get; }
    }
}