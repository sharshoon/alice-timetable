using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        protected override SimpleResponse Respond(AliceRequest request, State state)
        {
            state.Step = Step.None;


            return new SimpleResponse()
            {
                Text = Date.ToString()
            };
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
            var weekDay = tokens.FirstOrDefault(token => weekDays.Any(day => token.StartsWith(day)));
            if (weekDay != null)
            {
                var date = DateTime.Today.AddDays(weekDays.IndexOf(weekDay) - (int)DateTime.Today.DayOfWeek);
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
                        // Если написать, то 06.03.2001 то по идее работать пока не будет
                        var months = new List<string>
                        {
                            "январ",
                            "феврал",
                            "март",
                            "апрел",
                            "мая",
                            "июн",
                            "июл",
                            "август",
                            "сентябр",
                            "октябр",
                            "ноябр",
                            "декабр",
                            "1","2","3","4","5","6",
                            "7","8","9","10","11","12"
                        };

                        var month = tokens
                            .Skip(tokens.IndexOf(day) + 1)
                            .FirstOrDefault(token => months.Any(month => token.StartsWith(month)));

                        if (month != null && tokens.IndexOf(month) + 1 == tokens.Count())
                        {
                            return DateTime.TryParse($"{day} {month}", out Date);
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
