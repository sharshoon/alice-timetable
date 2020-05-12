using alice_timetable.Models;
using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace alice_timetable.Engine.Modifiers
{
    public class ModifierSayTimetable : ModifierSayTimetableBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            if (state.Step != Step.None)
            {
                return false;
            }

            var keywords = new List<string>
            {
                "расписание сегодня",
                "расписание завтра",
                "расписание послезавтра",
                "расписание в",
                "расписание на",
                "расписание во",
                "расписание"
            };

            var requestString = request.Request.Nlu.Tokens;
            var hasKeWorlds = keywords.Any(kw =>
            {
                var tokens = kw.Split(" ");
                return tokens.All(requestString.ContainsStartWith);
            });

            if (!hasKeWorlds)
            {
                return false;
            }

            return DateCheck(requestString);
        }

        protected override SimpleResponse Respond(AliceRequest request, ISchedulesRepository schedulesRepo, State state)
        {
            state.Step = Step.None;

            var schedule = SchedulesRepository.Schedules.FirstOrDefault(item => item.Group == int.Parse(state.User.Group));
            if (schedule == null)
            {
                using var client = new HttpClient();
                var bsuirStringResponse = client
                    .GetStringAsync($"https://journal.bsuir.by/api/v1/studentGroup/schedule?studentGroup={state.User.Group}")
                    .Result;
                schedule = JsonConvert.DeserializeObject<BsuirScheduleResponse>(bsuirStringResponse);
                schedule.Group = int.Parse(schedule.studentGroup.name);

                schedulesRepo.AddSchedule(schedule);
            }

            if(Date > DateTime.Parse(schedule.dateEnd) || Date < DateTime.Parse(schedule.dateStart))
            {
                return new SimpleResponse()
                {
                    Text = "Вы указали слишком большую, или слишком маленьку дату, которая не входит в ваш учебный семестр!"
                };
            }

            var responseText = FormResponse(SchedulesRepository.CurrentWeek, schedule.schedules, state);

            responseText = String.IsNullOrWhiteSpace(responseText)
                ? "В этот день нет ни одной пары"
                : responseText;

            var response = new SimpleResponse()
            {
                Text = $"{responseText}"
            };
            return response;
        }
    }
}
