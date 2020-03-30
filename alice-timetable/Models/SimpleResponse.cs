using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Models
{
    public class SimpleResponse
    {
        public string Text { get; set; }
        public string Tts { get; set; }
        public string[] Buttons { get; set; }
    }
}
