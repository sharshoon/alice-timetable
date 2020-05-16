using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Engine.Modifiers.SettingsModifiers
{
    public class ModifierGetNewName : ModifierBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            return state.Step == Step.AwaitForNewName && !String.IsNullOrWhiteSpace(request.Request.Command);
        }

        protected override SimpleResponse Respond(AliceRequest request, ISchedulesRepository schedulesRepo, State state)
        {
            state.Step = Step.None;
            state.User.Name = request.Request.Command;

            return new SimpleResponse()
            {
                Text = $"Принято, {state.User.Name}"
            };
        }
    }
}
