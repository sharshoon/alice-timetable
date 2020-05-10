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
    public class ModifierGetName : ModifierBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            return request.Request.Command.Trim() != "" && state.Step == Step.AwaitForName;
        }

        protected override SimpleResponse Respond(AliceRequest request, ISchedulesRepository schedulesRepo, State state)
        {
            state.User.Name = request.Request.Command;
            state.Step = Step.AwaitForGroup;


            return new SimpleResponse()
            {
                Text = "А теперь скажи, какая у тебя группа?"
            };
        }
    }
}
