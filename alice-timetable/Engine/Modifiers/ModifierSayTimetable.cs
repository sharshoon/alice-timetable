using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Engine.Modifiers
{
    public class ModifierSayTimetable : ModifierBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            if (state.Step != Step.None)
            {
                return false;
            }
            return request.Request.Command == "Показать расписание";
        }

        protected override SimpleResponse Respond(AliceRequest request, State state)
        {
            state.Step = Step.AwaitForTimetable;
            return new SimpleResponse()
            {
                Text = "РАСПИСАНИЕ"
            };
        }
    }
}
