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
    public class ModifierGetGroup : ModifierBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            return request.Request.Command != "" && state.Step == Step.AwaitForGroup;
        }

        protected override SimpleResponse Respond(AliceRequest request, ISchedulesRepository schedulesRepo, State state)
        {
            var result = 0;
            var commandText = request.Request.Command.Replace(" ", "");
            if (commandText.Length == 6 && int.TryParse(commandText, out result))
            {
                state.User.Group = result.ToString();
                state.Step = Step.AwaitForCustomizationAnswer;

                return new SimpleResponse()
                {
                    Text = $"Хорошо {state.User.Name}, мы готовы к работе! \n" +
                            $"Но, перед тем, как начать, я хотела бы спросить тебя о том, " +
                            $"не хотел бы настроить то, какую конкретно информацию я буду тебе озвучивать? " +
                            $"Ты можешь пропустить это шаг и тогда я буду действовать по умолчанию, " +
                            $"но, не бойся, ты всегда сможешь поменять эти настройки"
                };
            }
            else
            {
                state.Step = Step.AwaitForGroup;

                return new SimpleResponse()
                {
                    Text =  $"Хм, {state.User.Name}, мне кажется, что ты ввел неправильную группу." +
                            $"Группа должна состоять из 6 цифр, к примеру '851005'\n" +
                            $"Попробуй еще раз"
                };
            }
        }
    }
}
