using alice_timetable.Engine;
using alice_timetable.Models;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Engine.Modifiers
{
    public class ModifierUnknown : ModifierBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            return true;
        }

        protected override SimpleResponse Respond(AliceRequest request, ISchedulesRepository schedulesRepo, State state)
        {
            return new SimpleResponse()
            {
                Text = $"Не поняла"
            };
        }
    }
}
