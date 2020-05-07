using alice_timetable;
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

            var keywords = new List<string>
            {
                "сказать расписание",
                "скажи расписание",
                "пока расписание",
                "какое расписание",
                "выведи расписание",
                "озвучь расписание"
            };

            var requestString = request.Request.Nlu.Tokens;

            return keywords.Any(kw =>
            {
                var tokens = kw.Split(" ");
                return tokens.All(requestString.ContainsStartWith);
            });
            
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
 