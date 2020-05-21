using alice_timetable.Engine;
using alice_timetable.Models;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Engine.Modifiers
{
    public class ModifierEnter : ModifierBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            return request.Request.Command == "";
        }

        protected override SimpleResponse Respond(AliceRequest request, ISchedulesRepository schedulesRepo,  State state)
        {
            state.Step = Step.None;
            return new SimpleResponse()
            {
                Text = "Приветствую, чего желаете?"
            };
        }
    }
}
