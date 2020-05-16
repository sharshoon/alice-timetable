using alice_timetable.Models;
using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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

                if(String.IsNullOrWhiteSpace(bsuirStringResponse))
                {
                    return new SimpleResponse()
                    {
                        Text = "Похоже, что расписания вашей группы нет на сервере, сочувствую :("
                    };
                }
                schedule = JsonConvert.DeserializeObject<BsuirScheduleResponse>(bsuirStringResponse);
                schedule.Group = int.Parse(schedule.studentGroup.name);

                schedulesRepo.AddSchedule(schedule);
            }

            if(Date > DateTime.Parse(schedule.dateEnd, CultureInfo.GetCultureInfo("ru-RU")) || Date < DateTime.Parse(schedule.dateStart, CultureInfo.GetCultureInfo("ru-RU")))
            {
                return new SimpleResponse()
                {
                    Text = $"Вы указали слишком большую, или слишком маленьку дату ({Date.ToString("d", CultureInfo.GetCultureInfo("ru-RU"))}), которая не входит в ваш учебный семестр!"
                };
            }

            var mondayThisWeek = DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek == 0 ? 6 : (int)DateTime.Today.DayOfWeek - 1));
            var weeksAdd = (Date - mondayThisWeek).Days / 7 ;
            var week = (weeksAdd + SchedulesRepository.CurrentWeek - 1) % 4 + 1;

            var responseText = FormResponse(week, schedule.schedules, state);

            responseText = String.IsNullOrWhiteSpace(responseText)
                ? "В этот день нет ни одной пары"
                : responseText;

            var response = new SimpleResponse()
            {
                Text = $"{responseText}\n{Date.ToString("d", CultureInfo.GetCultureInfo("ru-RU"))}"
            };
            return response;
        }
    }
}
