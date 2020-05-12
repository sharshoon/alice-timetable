using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Engine
{
    public class State
    {
        public User User { get; set; } = new User();
        public Step Step { get; set; } = Step.None;

    }
    public enum Step
    {
        None,
        AwaitForName,
        AwaitForGroup,
        AwaitForCustomizationAnswer,
        AwaitForDisplayingAuditory,
        AwaitForDisplayingSubjectTime,
        AwaitForDisplayingEmployee,
        AwaitForDisplayingSubjectType
    }
}
