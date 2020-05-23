using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Engine.Modifiers
{
    public class ModifierScheduleLeftToday : ModifierSayTimetableBase
    {
        private readonly int timeZone = 3;
        protected override bool Check(AliceRequest request, State state)
        {
            if (state.Step != Step.None)
            {
                return false;
            }

            var keywords = new List<string>
            {
                "какие пары остались",
                "что осталось"
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
            var responseText = GetResponse(schedulesRepo, state);

            return new SimpleResponse()
            {
                Text = $"{responseText}"
            };
        }

        private string GetResponse(ISchedulesRepository schedulesRepo, State state)
        {
            state.Step = Step.None;
            string errorMessage;
            Date = DateTime.UtcNow.AddHours(timeZone);

            var schedule = schedulesRepo.GetSchedule(int.Parse(state.User.Group)).Result;
            if (schedule == null)
            {
                if (!TrySendScheduleResponse(schedulesRepo, state, out schedule, out errorMessage))
                {
                    return errorMessage;
                }
            }

            var responseText = GetTodayScheduleLeft(SchedulesRepository.CurrentWeek, schedule.schedules, state.User.DisplayAuditory,
                                            state.User.DisplayEmployeeName, state.User.DisplaySubjectTime, state.User.DisplaySubjectType);

            responseText = String.IsNullOrWhiteSpace(responseText)
                ? "На сегодня больше нет пар!"
                : responseText;

            return responseText;
        }

        private string GetTodayScheduleLeft(int week, IList<Schedule> schedules, bool displayAuditory, bool displayEmployeeName, bool displaySubjectTime, bool displaySubjectType)
        {
            var responseText = "";
            // 0 - Sunday
            if ((int)Date.DayOfWeek != 0 && schedules.Count() > 0)
            {
                var schedule = GetScheduleByDayName(schedules);
                if (schedule == null)
                {
                    return "";
                }

                var number = 1;
                foreach (var item in schedule.schedule)
                {
                    if (item.weekNumber.Contains(week))
                    {
                        if (DateTime.Parse(item.startLessonTime, CultureInfo.GetCultureInfo("ru-RU")).TimeOfDay > Date.TimeOfDay)
                        {
                            responseText = FormDisplayingParameters(responseText, number, item, displayAuditory, displayEmployeeName,
                                                                    displaySubjectTime, displaySubjectType);
                            number++;
                        }
                        else if ((DateTime.Parse(item.startLessonTime, CultureInfo.GetCultureInfo("ru-RU")).TimeOfDay < Date.TimeOfDay) &&
                             (DateTime.Parse(item.endLessonTime, CultureInfo.GetCultureInfo("ru-RU")).TimeOfDay > Date.TimeOfDay))
                        {
                            responseText = FormDisplayingParameters(responseText, number, item, displayAuditory, displayEmployeeName,
                                                                     displaySubjectTime, displaySubjectType);
                            responseText += "Внимание, эта пара уже началась!\n";
                        }
                    }
                }
            }

            return responseText;
        }
    }
}
