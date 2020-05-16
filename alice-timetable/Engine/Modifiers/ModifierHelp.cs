using alice_timetable.Engine.Modifiers.Helpers;
using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable.Engine.Modifiers
{
    public class ModifierHelp : ModifierBase
    {
        private List<Helper> helpers = new List<Helper>
        {
            new HelperNone(),
            new HelperAwaitForGroup(),
            new HelperAwaitForName(),
            new HelperCustomization(),
            new HelperDisplayAuditory(),
            new HelperDisplayEmployee(),
            new HelperDisplaySubjectTime(),
            new HelperDisplaySubjectType(),
        };
        protected override bool Check(AliceRequest request, State state)
        {
            var keywords = new List<string>
            {
                "помощь",
                "помоги"
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
                Text = GetHelper(state)
            };
        }

        private string? GetHelper(State state)
        {
            var helper = helpers.FirstOrDefault(helper => helper.Steps.Any(step => step == state.Step));
            if (helper != null)
            {
                return helper.GetHelper();
            }

            return null;
        }
    }
}
