using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Engine
{
    public class State
    {
        public User User { get; set; }
        public Step Step { get; set; } = Step.None;

    }
    public enum Step
    {
        None, 
        AwaitForTimetable
    }
}
