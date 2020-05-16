using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using System.Collections.Generic;
using System.Linq;

namespace alice_timetable.Engine.Modifiers.SettingsModifiers
{
    public class ModifierChangeGroup : ModifierBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            if (state.Step != Step.None)
            {
                return false;
            }

            var keywords = new List<string>()
            {
                "сменить группу",
                "изменить группу",
                "другая группа"
            };

            var requestString = request.Request.Nlu.Tokens;
            return keywords.Any(kw =>
            {
                var tokens = kw.Split(" ");
                return tokens.All(requestString.ContainsStartWith);
            });
        }

        protected override SimpleResponse Respond(AliceRequest request, ISchedulesRepository schedulesRepo, State state)
        {
            state.Step = Step.AwaitForNewGroup;
            return new SimpleResponse()
            {
                Text = "Скажи номер новой группы"
            };
        }
    }
}
