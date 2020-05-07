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
            new ModifierUnknown()
        };
        private readonly State _state = new State();
        public UserSession(string userID, IUsersRepository repo)
        {
            repository = repo;
            User user = new User();
            repository.CreateOrSaveUser(out user, userID);
            _state.User = user;

            Console.WriteLine($"Получена сессия пользователя {user.ID}");
        }

        public UserSession(string userID, IUsersRepository repo, State state)
        {
            repository = repo;
            User user = new User();
            repository.CreateOrSaveUser(out user, userID);
            state.User = user;
            _state = state;

            Console.WriteLine($"Получена сессия пользователя {user.ID}");
        }

        internal AliceResponse HandleRequest(AliceRequest aliceRequest, ref State state)
        {
            AliceResponse response = null;
            if(!Modifiers.Any(modifier => modifier.Run(aliceRequest, _state, out response)))
            {
                throw new NotSupportedException("No default modifier");
            }

            state = _state;
            //Нуrepository.UpdateUser(_state.User);
            //Console.WriteLine("Пользователь обновлен");

            return response;
        }
    }
}
