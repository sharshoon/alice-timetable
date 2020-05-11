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
    public class ModifierDisplaySubjectType : ModifierBase 
    {
        protected override bool Check(AliceRequest request, State state)
        {
            if (state.Step != Step.AwaitForDisplayingSubjectType)
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
                state.User.DisplaySubjectType = true;
            }
            else
            {
                state.User.DisplaySubjectType = false;
            }

            state.Step = Step.AwaitForDisplayingEmployee;
            return new SimpleResponse()
            {
                Text =  $"Ну и последнее, {state.User.Name}, \n" +
                        $"Хочешь , чтобы я называла имя преподавателя, который будет вести занятие?"
            };
        }
    }
}
