using alice_timetable.Models;
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
    public abstract class ModifierSayTimetableBase : ModifierBase
    {
        protected DateTime Date;
        protected override abstract bool Check(AliceRequest request, State state);

        protected override abstract SimpleResponse Respond(AliceRequest request, ISchedulesRepository schedulesRepo, State state);

        protected string FormResponse(int week, IList<Schedule> schedules, State state)
        {
            // Делим на 7, получаем кол-во недель с начала семестра
            // делаем mod 4 + 1, чтобы получить номер учебной недели
            //var currentWeek = (Date - DateTime.Parse(dateStart)).Days / 7 % 4 + 1;
            //var dayNumber = (int)Date.DayOfWeek;

            var responseText = "";
            // 0 - Sunday
            if ((int)Date.DayOfWeek != 0 && schedules.Count() > 0)
            {
                var weekDays = new Dictionary<string, string>
                {
                    ["понедельник"] = "monday",
                    ["вторник"] = "tuesday",
                    ["среда"] = "wednesday",
                    ["четверг"] = "thursday",
                    ["пятница"] = "friday",
                    ["суббота"] = "saturday",
                    ["воскресенье"] = "sunday"
                };
                var schedule = schedules.FirstOrDefault(schedule => weekDays[schedule.weekDay.ToLower()] == Date.DayOfWeek.ToString().ToLower());
                if (schedule == null)
                {
                    return "";
                }

                var number = 1;
                foreach (var item in schedule.schedule)
                {
                    if (item.weekNumber.Contains(week))
                    {
                        responseText += $"{number}. {item.subject}";

                        responseText += item.numSubgroup != 0 ? $" ({item.numSubgroup} подгруппа) \n" : "\n";

                        responseText += state.User.DisplaySubjectType ? item.lessonType + "\n" : "";
                        responseText += state.User.DisplaySubjectTime ? item.lessonTime + "\n" : "";
                        responseText += state.User.DisplayAuditory ? String.Join("", item.auditory) + "\n" : "";
                        responseText += state.User.DisplayEmployeeName && item.employee.Count > 0 ?
                            $" {item.employee[0].lastName}  {item.employee[0].firstName} {item.employee[0].middleName} \n"
                            : "";

                        responseText += "\n";
                        number++;
                    }
                }
            }

            return responseText;
        }

        protected bool DateCheck(IList<string> tokens)
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
                var todayDay = (int)DateTime.Today.DayOfWeek == 0 ? 7 : (int)DateTime.Today.DayOfWeek;
                var daysToAdd = (index - todayDay) >= 0 ? index - todayDay : 7 - Math.Abs(index - todayDay);

                var date = DateTime.Today.AddDays(daysToAdd);
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
                        if (DateTime.TryParse(String.Join(".", tokens.Skip(tokens.IndexOf(day))),CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out Date))
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
