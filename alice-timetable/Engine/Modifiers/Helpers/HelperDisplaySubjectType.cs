using Alice_Timetable.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Engine.Modifiers.Helpers
{
    public class HelperDisplaySubjectType : Helper
    {
        public override Step[] Steps => new Step[] { Step.AwaitForDisplayingSubjectType };

        public override string GetHelper()
        {
            return "Выбери, буду ли я озвучивать тип занятия занятия. Занятия могут быть лекции, лабораторные или практические" +
                    "Варианты ответа могут быть такими: 'да', 'нет', 'хочу', 'хотел бы', 'не хочу', 'не хотел бы', 'хорошо'";
        }
    }
}
