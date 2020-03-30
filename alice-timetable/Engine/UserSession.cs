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
            new ModifierEnter(),
            new ModifierSayTimetable(),
            new ModifierUnknown()
        };
        private readonly State _state = new State();
        public UserSession(string userID, IUsersRepository repo)
        {
            repository = repo;
            User user = new User();
            repository.CreateOrSaveUser(user, userID);
            _state.User = user;
        }

        internal AliceResponse HandleRequest(AliceRequest aliceRequest)
        {
            AliceResponse response = null;
            if(!Modifiers.Any(modifier => modifier.Run(aliceRequest, _state, out response)))
            {
                throw new NotSupportedException("No default modifier");
            }
            return response;
        }
    }
}
