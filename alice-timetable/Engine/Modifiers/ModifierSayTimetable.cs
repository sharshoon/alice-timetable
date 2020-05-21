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

        private string FormExamSchedule(ExamSchedule exam, User user)
        {
            string responseText = "";
            int number = 1;
            foreach(var item in exam.schedule)
            {
                responseText += $"{number}. '{item.subject}'";

                responseText += item.numSubgroup != 0 ? $" ({item.numSubgroup} подгруппа) \n" : "\n";

                responseText += user.DisplayAuditory ? item.lessonType + "\n" : "";
                responseText += user.DisplaySubjectTime ? item.lessonTime + "\n" : "";
                responseText += user.DisplayEmployeeName ? String.Join("", item.auditory) + "\n" : "";
                responseText += user.DisplaySubjectType && item.employee.Count > 0 ?
                    $" {item.employee[0].lastName}  {item.employee[0].firstName} {item.employee[0].middleName} \n"
                    : "";

                responseText += "\n";
                number++;
            }

            return responseText;
        }

        private string GetResponse(ISchedulesRepository schedulesRepo, State state)
        {
            state.Step = Step.None;

            var schedule = schedulesRepo.GetSchedule(int.Parse(state.User.Group)).Result;
            if (schedule == null)
            {
                using var client = new HttpClient();

                var bsuirResponse = client.GetAsync($"https://journal.bsuir.by/api/v1/studentGroup/schedule?studentGroup={state.User.Group}").Result;
                if (bsuirResponse.IsSuccessStatusCode)
                {
                    var stringResponse = bsuirResponse.Content.ReadAsStringAsync().Result;
                    if (String.IsNullOrWhiteSpace(stringResponse))
                    {
                        return "Похоже, что расписания вашей группы нет на сервере, сочувствую :(";
                    }
                    schedule = JsonConvert.DeserializeObject<BsuirScheduleResponse>(stringResponse);
                    schedule.Group = int.Parse(schedule.studentGroup.name);

                    schedulesRepo.AddSchedule(schedule);
                }
                else
                {
                    return $"Простите, сервер не отвечает, а сохраненного расписания этой группы у меня нет :(((";
                }
            }

            if (Date > DateTime.Parse(schedule.dateEnd, CultureInfo.GetCultureInfo("ru-RU")) || Date < DateTime.Parse(schedule.dateStart, CultureInfo.GetCultureInfo("ru-RU")))
            {
                if (schedule.examSchedules.Count() != 0)
                {
                    var exam = schedule.examSchedules.FirstOrDefault(exam => DateTime.Parse(exam.weekDay, CultureInfo.GetCultureInfo("ru-RU")) == Date);

                    if (exam == null)
                    {
                        return "В этот день нет ни одной пары";   
                    }
                    else
                    {
                        return FormExamSchedule(exam, state.User);
                    }
                }

                return $"Вы указали слишком большую, или слишком маленьку дату ({Date.ToString("d", CultureInfo.GetCultureInfo("ru-RU"))}), которая не входит в ваш учебный семестр!";
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
