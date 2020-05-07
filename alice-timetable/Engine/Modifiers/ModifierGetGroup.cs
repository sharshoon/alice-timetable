using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Engine.Modifiers
{
    public class ModifierGetGroup : ModifierBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            return request.Request.Command != "" && state.Step == Step.AwaitForGroup;
        }

        protected override SimpleResponse Respond(AliceRequest request, State state)
        {
            state.User.Group = request.Request.Command;
            state.Step = Step.None;

            return new SimpleResponse()
            {
                Text = $"Хорошо {state.User.Name}, мы готовы к работе!"
            };
        }
    }
}
