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
            var responseText = GetResponse(schedulesRepo, state);
            var response = new SimpleResponse()
            {
                Text = $"{responseText}\n{Date.ToString("d", CultureInfo.GetCultureInfo("ru-RU"))}"
            };
            return response;
        }


        private string GetResponse(ISchedulesRepository schedulesRepo, State state)
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

            string result;
            if (!DateBoundariesCheck(state, schedule, out result, out errorMessage))
            {
                return errorMessage;
            }
            else if (!String.IsNullOrWhiteSpace(result))
            {
                return result;
            }

            return GetSchedule(schedule, state);
        }

        private string GetSchedule(BsuirScheduleResponse schedule, State state)
        {
            var mondayThisWeek = DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek == 0 ? 6 : (int)DateTime.Today.DayOfWeek - 1));
            var weeksAdd = (Date - mondayThisWeek).Days / 7;
            var week = (weeksAdd + SchedulesRepository.CurrentWeek - 1) % 4 + 1;

            var responseText = FormResponse(week, schedule.schedules, state.User.DisplayAuditory,
                                            state.User.DisplayEmployeeName, state.User.DisplaySubjectTime, state.User.DisplaySubjectType);

            responseText = String.IsNullOrWhiteSpace(responseText)
                ? "В этот день нет ни одной пары"
                : responseText;

            return responseText;
        }
    }
}
