using alice_timetable.Engine.Modifiers;
using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Engine
{
    public class UserSession
    {
        IUsersRepository repository;
        private static readonly List<ModifierBase> Modifiers = new List<ModifierBase>()
        {
            new ModifierFirstEnter(),
            new ModifierGetName(),
            new ModifierGetGroup(),
            new ModifierEnter(),
            new ModifierSayTimetable(),
            new ModifierAskTimetableDate(),
            new ModifierUnknown()
        };
        private readonly State _state = new State();

        public UserSession(string userID, IUsersRepository repo, State state)
        {
            repository = repo;

            Console.WriteLine($"Имя "+state.User.Name);
            _state.User = repository.CreateOrSaveUser(state.User, userID);

            Console.WriteLine($"Получена сессия пользователя {_state.User.ID}");
        }

        internal AliceResponse HandleRequest(AliceRequest aliceRequest, ref State state)
        {
            _state.Step = state.Step;
            AliceResponse response = null;
            if(!Modifiers.Any(modifier => modifier.Run(aliceRequest, _state, out response)))
            {
                throw new NotSupportedException("No default modifier");
            }

            state = _state;

            return response;
        }
    }
}
