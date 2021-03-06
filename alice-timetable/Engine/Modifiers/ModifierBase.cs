﻿using alice_timetable.Engine;
using alice_timetable.Models;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Engine.Modifiers
{
    public abstract class ModifierBase
    {
        public bool Run(AliceRequest request, State state, ISchedulesRepository schedulesRepo ,out AliceResponse response)
        {
            if (!Check(request, state))
            {
                response = null;
                return false;
            }
            response = CreateResponse(request, schedulesRepo,state);
            return true;
        }

        private AliceResponse CreateResponse(AliceRequest request, ISchedulesRepository schedulesRepo, State state)
        {
            var response = new AliceResponse(request);
            var simple = Respond(request, schedulesRepo, state);

            response.Response.Text = simple.Text;
            response.Response.Tts = String.IsNullOrEmpty(simple.Tts) ? simple.Text : simple.Tts;
            if (simple.Buttons != null)
            {
                response.Response.Buttons = simple.Buttons.Select(t => new Button { Title = t }).ToList();
            }

            return response;
        }

        protected abstract bool Check(AliceRequest request, State state);
        protected abstract SimpleResponse Respond(AliceRequest request, ISchedulesRepository schedulesRepo, State state);
    }
}
