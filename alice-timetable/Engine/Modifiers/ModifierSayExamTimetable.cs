using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Engine.Modifiers
{
    public class ModifierSayExamTimetable : ModifierSayTimetableBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            if (state.Step != Step.None)
            {
                return false;
            }

            var keywords = new List<string>
            {
                "пока экзамен",
                "расписание экзаменов"
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
            var responseText = GetExamSchedule(schedulesRepo, state);

            return new SimpleResponse()
            {
                Text = responseText
            };
        }

        private string GetExamSchedule(ISchedulesRepository schedulesRepo, State state)
        {
            state.Step = Step.None;
            string errorMessage;

            var schedule = schedulesRepo.GetSchedule(int.Parse(state.User.Group)).Result;
            if (schedule == null)
            {
                if (!TrySendScheduleResponse(schedulesRepo, state, out schedule, out errorMessage))
                {
                    return errorMessage;
                }
            }

            var responseText = "";

            var number = 1;
            foreach(var exams in schedule.examSchedules)
            {
                // number - какое по счету мы оглашаем расписание в нашем ответе
                responseText += FormExamSchedule(exams, state.User, number);
                number++;
            }

            return responseText;
        }
    }
}
