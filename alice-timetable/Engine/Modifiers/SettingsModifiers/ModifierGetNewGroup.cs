using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using System;

namespace alice_timetable.Engine.Modifiers.SettingsModifiers
{
    public class ModifierGetNewGroup : ModifierBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            return state.Step == Step.AwaitForNewGroup && !String.IsNullOrWhiteSpace(request.Request.Command);
        }

        protected override SimpleResponse Respond(AliceRequest request, ISchedulesRepository schedulesRepo, State state)
        {
            state.Step = Step.None;
            state.User.Group = request.Request.Command;

            return new SimpleResponse()
            {
                Text = $"Принято, теперь твоя группа - {state.User.Group}"
            };
        }
    }
}
