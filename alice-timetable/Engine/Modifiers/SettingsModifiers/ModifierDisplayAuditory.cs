using alice_timetable.Models;
using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Engine.Modifiers.SettingsModifiers
{
    public class ModifierDisplayAuditory : ModifierBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            if (state.Step != Step.AwaitForDisplayingAuditory)
            {
                return false;
            }

            var keywords = new List<string>
            {
                "да",
                "нет",
                "хочу",
                "хотел бы",
                "не хочу",
                "не хотел бы",
                "хорошо"
            };

            return keywords.Any(keyword =>
            {
                var tokens = keyword.Split(" ");
                return tokens.All(request.Request.Nlu.Tokens.ContainsStartWith);
            });
        }

        protected override SimpleResponse Respond(AliceRequest request, ISchedulesRepository schedulesRepo, State state)
        {
            var positive = new List<string>
            {
                "да",
                "хочу",
                "хотел бы",
                "начать",
                "хорошо"
            };

            var isPositive = positive.Any(item =>
            {
                var tokens = item.Split(" ");
                return tokens.All(request.Request.Nlu.Tokens.ContainsStartWith);
            });

            if (isPositive)
            {
                state.User.DisplayAuditory = true;
            }
            else
            {
                state.User.DisplayAuditory = false;
            }

            state.Step = Step.AwaitForDisplayingSubjectTime;
            return new SimpleResponse()
            {
                Text = "Принято, а как насчет того, чтобы я говорила о времени начала и окончания пары?"
            };
        }
    }
}
