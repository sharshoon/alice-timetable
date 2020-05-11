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
    public class ModifierSayTimetable : ModifierBase
    {
        private DateTime Date;
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
                var client = new HttpClient();
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

            // Делим на 7, получаем кол-во недель с начала семестра
            // делаем mod 4 + 1, чтобы получить номер учебной недели
            var currentWeek = (Date - DateTime.Parse(schedule.dateStart)).Days / 7 % 4 + 1;
            var dayNumber = (int)Date.DayOfWeek;

            var responseText = "";
            // 0 - Sunday
            if (dayNumber != 0) 
            {
                var number = 1;
                foreach (var item in schedule.schedules[dayNumber - 1].schedule)
                {
                    if (item.weekNumber.Contains(currentWeek))
                    {
                        responseText += $"{number}. {item.subject}";
                        
                        responseText += item.numSubgroup != 0 ? $" ({item.numSubgroup} подгруппа) \n" : "\n";

                        responseText += state.User.DisplaySubjectType ? item.lessonType + "\n" : "";
                        responseText += state.User.DisplaySubjectTime ? item.lessonTime + "\n": "";
                        responseText += state.User.DisplayAuditory ? String.Join("", item.auditory) + "\n" : "";
                        responseText += state.User.DisplayEmployeeName && item.employee.Count > 0 ? 
                            $" {item.employee[0].lastName}  {item.employee[0].firstName} {item.employee[0].middleName} \n" 
                            : "";

                        responseText += "\n";
                        number++;
                    }
                }
            }

            responseText = String.IsNullOrWhiteSpace(responseText)
                ? "В этот день нет ни одной пары"
                : responseText;

            var response = new SimpleResponse()
            {
                Text = $"{responseText}"
            };
            return response;
        }
        
        private bool DateCheck(IList<string> tokens)
        {
            // расписание сегодня (завтра, послезавтра)
            // расписание на сегодня (завтра, послезавтра, понедельник, вторник ...)
            // расписание в понедельник (вторник, среда ...)
            // расписание в следующий понедельник (вторник, среда ...)
            // расписание на 19.03.2020
            // расписение на 19 марта
            // расписание 19 марта (...)

            var weekDays = new List<string>()
            {
                "понедельник", "вторник", "сред", "четверг", "пятниц", "суббот", "воскресенье"
            };

            // Если пользователь задал какой-то конкретный день недели
            var weekDay = weekDays.FirstOrDefault(item => tokens.Any(token => token.StartsWith(item)));
            if (weekDay != null)
            {
                var index = weekDays.IndexOf(weekDay) + 1;
                var todayDay = (int)DateTime.Today.DayOfWeek;
                var date = DateTime.Today.AddDays(Math.Abs(index - todayDay));
                Date = date;

                return true;
            }
            else
            {
                var nearDays = new List<string>()
                {
                    "сегодня", "завтра", "послезавтра"
                };

                var nearDay = tokens.FirstOrDefault(token => nearDays.Contains(token));                
                if (nearDay != null)
                {
                    var date = DateTime.Today.AddDays(nearDays.IndexOf(nearDay));
                    Date = date;
                    return true;
                }
                else
                {
                    var dateEnds = new List<string>
                    {
                        "1",
                        "2",
                        "3",
                        "4",
                        "5",
                        "6",
                        "7",
                        "8",
                        "9",
                        "0"
                    };

                    var day = tokens.FirstOrDefault(token => dateEnds.Any(end => token.StartsWith(end)));
                    if (day != null)
                    {
                        // Если ввели если месяц ввели числом
                        if (DateTime.TryParse(String.Join(".", tokens.Skip(tokens.IndexOf(day))), out Date))
                        {
                            //// Чтобы нельзя было посмотреть расписание на предыдущие даты
                            //if(Date < DateTime.Now.AddDays(-1))
                            //{
                            //    return false;
                            //}

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
