using Alice_Timetable.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Engine.Modifiers.Helpers
{
    public class HelperDisplayEmployee : Helper
    {
        public override Step[] Steps => new Step[] { Step.AwaitForDisplayingEmployee };

        public override string GetHelper()
        {
            return "Выбери, буду ли я озвучивать преподавателя, который будет проводить занятие. " +
                    "Варианты ответа могут быть такими: 'да', 'нет', 'хочу', 'хотел бы', 'не хочу', 'не хотел бы', 'хорошо'";
        }
    }
}
