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
    public abstract class ModifierSayTimetableBase : ModifierBase
    {
        protected DateTime Date;
        protected override abstract bool Check(AliceRequest request, State state);

        protected override abstract SimpleResponse Respond(AliceRequest request, ISchedulesRepository schedulesRepo, State state);
        
        // Пробует преобразовать строку пользователя в конкретную дату
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
                        return DateTime.TryParse(String.Join(".", tokens.Skip(tokens.IndexOf(day))), CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out Date);

                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        protected string FormResponse(int week, IList<Schedule> schedules, bool displayAuditory, bool displayEmployeeName, bool displaySubjectTime, bool displaySubjectType)
        {
            // Делим на 7, получаем кол-во недель с начала семестра
            // делаем mod 4 + 1, чтобы получить номер учебной недели
            //var currentWeek = (Date - DateTime.Parse(dateStart)).Days / 7 % 4 + 1;
            //var dayNumber = (int)Date.DayOfWeek;

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
                        responseText = FormDisplayingParameters(responseText, number, item, displayAuditory, displayEmployeeName, 
                                                                displaySubjectTime, displaySubjectType);
                        number++;
                    }
                }
            }

            return responseText;
        }

        protected Schedule GetScheduleByDayName(IList<Schedule> schedules)
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
                return null;
            }
            else
            {
                return schedule;
            }
        }

        // Формирует строку исходя из настроек пользователя о информации, которой ему нужно предоставлять по поводу каждого занятия
        protected string FormDisplayingParameters(string responseText, int number, Alice_Timetable.Models.Schedules.Schedule item, 
                                                  bool displayAuditory, bool displayEmployeeName, bool displaySubjectTime, bool displaySubjectType)
        {
            responseText += $"{number}. '{item.subject}'";

            responseText += item.numSubgroup != 0 ? $" ({item.numSubgroup} подгруппа) \n" : "\n";

            if (displaySubjectType)
            {
                var subjectTypes = new Dictionary<string, string>
                {
                    ["ПЗ"] = "Практическое занятие",
                    ["ЛР"] = "Лабораторная работа",
                    ["ЛК"] = "Лекция"
                };

                var type = subjectTypes.ContainsKey(item.lessonType) ? subjectTypes[item.lessonType] : item.lessonType;
                responseText += $"Тип: {type} \n";
            }
            responseText += displaySubjectTime ? item.lessonTime + "\n" : "";
            responseText += displayAuditory ? String.Join("", item.auditory) + "\n" : "";
            responseText += displayEmployeeName && item.employee.Count > 0 ?
                $" {item.employee[0].lastName}  {item.employee[0].firstName} {item.employee[0].middleName} \n"
                : "";

            responseText += "\n";

            return responseText;
        }

        // Формирует расписание экзаменов
        protected string FormExamSchedule(ExamSchedule exam, User user, int number)
        {
            string responseText = "";
            foreach (var item in exam.schedule)
            {
                responseText = FormDisplayingParameters(responseText, number, item, user.DisplayAuditory,
                                                        user.DisplayEmployeeName, user.DisplaySubjectTime, user.DisplaySubjectType);
                responseText = responseText.TrimEnd();
                responseText += "\n" + exam.weekDay + "\n\n"; 

                number++;
            }
            return responseText;
        }

        // Отправляет запрос на сервер с просьбой предоставить расписание
        protected bool TrySendScheduleResponse(ISchedulesRepository schedulesRepo, State state, out BsuirScheduleResponse result, out string errorMessage)
        {
            using var client = new HttpClient();
            var schedule = new BsuirScheduleResponse();
            var bsuirResponse = client.GetAsync($"https://journal.bsuir.by/api/v1/studentGroup/schedule?studentGroup={state.User.Group}").Result;
            if (bsuirResponse.IsSuccessStatusCode)
            {
                var stringResponse = bsuirResponse.Content.ReadAsStringAsync().Result;
                if (String.IsNullOrWhiteSpace(stringResponse))
                {
                    result = null;
                    errorMessage = "Похоже, что расписания вашей группы нет на сервере, сочувствую :(";
                    return false;
                }
                schedule = JsonConvert.DeserializeObject<BsuirScheduleResponse>(stringResponse);
                schedule.Group = int.Parse(schedule.studentGroup.name);

                schedulesRepo.AddSchedule(schedule);
            }
            else
            {
                result = null;
                errorMessage = $"Простите, сервер не отвечает, а сохраненного расписания этой группы у меня нет :(((";
                return false;
            }

            result = schedule;
            errorMessage = "";
            return true;
        } 

        // Проверяет входит ли дата, запрошенная пользователем в учебный семестр, если нет, то дополнительно проверяет доступность экзаменов
        protected bool DateBoundariesCheck(State state, BsuirScheduleResponse schedule, out string result, out string errorMessage)
        {
            if (Date > DateTime.Parse(schedule.dateEnd, CultureInfo.GetCultureInfo("ru-RU")) || Date < DateTime.Parse(schedule.dateStart, CultureInfo.GetCultureInfo("ru-RU")))
            {
                if (schedule.examSchedules.Count() != 0)
                {
                    var exam = schedule.examSchedules.FirstOrDefault(exam => DateTime.Parse(exam.weekDay, CultureInfo.GetCultureInfo("ru-RU")) == Date);

                    if (exam == null)
                    {
                        errorMessage = "В этот день нет ни одной пары";
                        result = "";
                        return false;
                    }
                    else
                    {
                        errorMessage = "";
                        // 1 - с какого номера начинать нумеровать выводимый список экзаменов
                        result = FormExamSchedule(exam, state.User, 1);
                        return true;
                    }
                }
                result = "";
                errorMessage = $"Вы указали слишком большую, или слишком маленьку дату ({Date.ToString("d", CultureInfo.GetCultureInfo("ru-RU"))}), которая не входит в ваш учебный семестр!";
                return false;
            }

            result = "";
            errorMessage = "";
            return true;
        }

    }
}
