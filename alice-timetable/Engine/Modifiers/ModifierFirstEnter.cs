using alice_timetable.Models;
using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Engine.Modifiers
{
    public class ModifierFirstEnter : ModifierBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            return request.Request.Command == "" && String.IsNullOrEmpty(state.User.Group);
        }

        protected override SimpleResponse Respond(AliceRequest request, ISchedulesRepository schedulesRepo, State state)
        {
            state.Step = Step.AwaitForName;
            return new SimpleResponse()
            {
                Text =  "Привет, мне кажется, что мы раньше не встречались. " +
                        "Я помогу тебе узнавать информацию о твоем расписании в университете, " +
                        "но для этого мне понадобится узнать от тебя несколько вещей. " +
                        "Для начала скажи, как мне тебя называть?"
            };
        }
    }
}
