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
    public class ModifierAskForCustomization : ModifierBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            if(state.Step != Step.AwaitForCustomizationAnswer) 
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
                "начать",
                "пропустить"
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
                "начать"
            };

            var isPositive = positive.Any(item =>
            {
                var tokens = item.Split(" ");
                return tokens.All(request.Request.Nlu.Tokens.ContainsStartWith);
            });

            if (isPositive)
            {
                state.Step = Step.AwaitForDisplayingAuditory;
                return new SimpleResponse()
                {
                    Text =  "Хочешь, чтобы когда ты спросил у меня расписание, я дополнительно озвучила тебе " +
                            "аудиторию, в котором будет проводиться занятие?"
                };
            }
            else
            {
                state.Step = Step.None;
                return new SimpleResponse()
                {
                    Text = "Ладно, как скажешь :("
                };
            }
        }
    }
}
