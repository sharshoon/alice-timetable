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
            int result;
            var commandText = request.Request.Command.Replace(" ", "");
            if (commandText.Length == 6 && int.TryParse(commandText, out result))
            {
                state.Step = Step.None;
                state.User.Group = result.ToString();

                return new SimpleResponse()
                {
                    Text = $"Принято, теперь твоя группа - {state.User.Group}"
                };
            }
            else
            {
                state.Step = Step.AwaitForNewGroup;

                return new SimpleResponse()
                {
                    Text =  $"Хм, {state.User.Name}, мне кажется, что ты ввел неправильную группу." +
                            $"Группа должна состоять из 6 цифр, к примеру '851005'\n" +
                            $"Попробуй еще раз!"
                };
            }
        }
    }
}
