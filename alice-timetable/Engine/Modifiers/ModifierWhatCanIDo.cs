using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using System.Collections.Generic;
using System.Linq;

namespace alice_timetable.Engine.Modifiers
{
    public class ModifierWhatCanIDo : ModifierBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            var keywords = new List<string>
            {
                "что ты умеешь",
                "что ты можешь",
                "как пользоватьс€"
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
            return new SimpleResponse()
            { 
                Text =  $"я могу помогать тебе отслеживать расписание твоей группы. \n " +
                        $"ƒл€ этого ты можешь использовать такие команды как 'расписание на завтра', 'расписание на четверг', 'расписание на 26.05' и так далее \n" +
                        $"я буду говорить тебе твое расписание согласно твоим предпочтени€м. Ќапример, если ты хочешь € могу упоминать, или не упоминать " +
                        $"номер аудитории, руководител€ зан€ти€, тип зан€ти€ и врем€ его проведени€. ¬се это ты настраиваешь при первом входе, " +
                        $"а так же можешь изменить их, сказав 'сменить параметры отображени€'. \n" +
                        $"Ѕолее того, ты так же можешь мен€ть то, как € теб€ называю, сказав 'сменить им€' и помен€ть номер своей группы, сказав 'сменить группу' \n" +
                        $" роме отображени€ расписани€ группы, € так же умею озвучивать расписание какого-то конкретного преподавател€. Ќапример: " +
                        $"'–асписание преподвател€ Ўаршун Ќикита на сегодн€' или '–асписание у Ўаршуна на сегодн€' \n" +
                        $"≈сли тебе будет что-то непон€тно, то на каждом своем шаге ты можешь сказать 'помощь' или 'помоги мне' и € расскажу, как тебе действовать"
            };
        }
    }
}