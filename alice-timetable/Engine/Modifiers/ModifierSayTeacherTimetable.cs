﻿using alice_timetable.Models;
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
    public class ModifierSayTeacherTimetable : ModifierSayTimetableBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            if (state.Step != Step.None)
            {
                return false;
            }

            var keywords = new List<string>
            {
                "расписание у",
                "расписание преподавателя сегодня",
                "расписание преподавателя завтра",
                "расписание преподавателя послезавтра",
                "расписание преподавателя в",
                "расписание преподавателя на",
                "расписание преподавателя во",
                "расписание преподавателя "
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
            var name = GetTeachersName(request.Request.Nlu.Tokens);
            var teacher = GetTeacherByName(schedulesRepo, name);

            if (teacher != null)
            {
                var schedule = schedulesRepo.GetTeacherSchedule(teacher.id).Result;
                if (schedule == null)
                {
                    using var client = new HttpClient();
                    var bsuirResponse = client
                        .GetAsync($"https://journal.bsuir.by/api/v1/portal/employeeSchedule?employeeId={teacher.id}")
                        .Result;

                    if (bsuirResponse.IsSuccessStatusCode)
                    {
                        var stringResponse = bsuirResponse.Content.ReadAsStringAsync().Result;
                        schedule = JsonConvert.DeserializeObject<TeacherScheduleResponse>(stringResponse);
                        schedule.EmployeeId = schedule.employee.id;
                        schedulesRepo.AddTeacherSchedule(schedule);
                    }
                    else
                    {
                        return new SimpleResponse()
                        {
                            Text = $"Простите, сервер не отвечает, а сохраненного расписания этого преподавателя у меня нет :((("
                        };
                    }
                }

                var mondayThisWeek = DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek == 0 ? 6 : (int)DateTime.Today.DayOfWeek - 1));
                var weeksAdd = (Date - mondayThisWeek).Days / 7;
                var week = (weeksAdd + SchedulesRepository.CurrentWeek - 1) % 4 + 1;

                var responseText = FormResponse(week, schedule.schedules, state.User.DisplayAuditory, false, 
                                                state.User.DisplaySubjectTime, state.User.DisplaySubjectType);

                responseText = String.IsNullOrWhiteSpace(responseText)
                    ? "В этот день у преподавателя нет ни одной пары"
                    : responseText + $"{teacher.lastName}  {teacher.firstName} {teacher.middleName} \n";

                return new SimpleResponse()
                {
                    Text = $"{responseText}"
                };
            }

            var response = new SimpleResponse()
            {
                Text = $"Я не знаю такого преподавателя :( "
            };
            return response;
        }

        private Teacher GetTeacherByName(ISchedulesRepository schedulesRepo, List<string> name)
        {
            if(name.Count() > 0)
            {
                return schedulesRepo.Teachers.FirstOrDefault(teacher =>
                    (name.Count() == 3 && teacher.lastName.StartsWith(name[0]) && teacher.firstName.StartsWith(name[1]) && teacher.middleName.StartsWith(name[2]))
                    || (name.Count() == 2 && teacher.lastName.StartsWith(name[0]) && teacher.firstName.StartsWith(name[1]))
                    || (name.Count() == 1 && teacher.lastName.StartsWith(name[0])));
            }
            return null;
        }

        private List<string> GetTeachersName(IList<string> tokens)
        {
            var timeKeywords = new List<string>()
            {
                "сегодня",
                "завтра",
                "послезавтра",
                "в",
                "на",
                "во"
            };

            var name = tokens
                .SkipWhile(token => token == "у" || token == "преподавателя")
                .Skip(2)
                .SkipWhile(token => token == "преподавателя")
                .TakeWhile(token => !timeKeywords.Contains(token))
                .Select(token => token[0..^1])
                .ToList();

            return name;
        }
    }
}
