using alice_timetable.Engine;
using alice_timetable.Engine.Modifiers;
using alice_timetable.Engine.Modifiers.SettingsModifiers;
using alice_timetable.Models;
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
        IUsersRepository usersRepository;
        ISchedulesRepository schedulesRepository;
        private static readonly List<ModifierBase> Modifiers = new List<ModifierBase>()
        {
            new ModifierFirstEnter(),
            new ModifierGetName(),
            new ModifierGetGroup(),
            new ModifierEnter(),
            new ModifierSayTeacherTimetable(),
            new ModifierSayTimetable(),
            new ModifierAskTimetableDate(),
            new ModifierDisplayAuditory(),
            new ModifierDisplaySubjectTime(),
            new ModifierDisplaySubjectType(),
            new ModifierDisplayEmployee(),
            new ModifierAskForCustomization(),
            new ModifierWhatCanIDo(),
            new ModifierChangeName(),
            new ModifierGetNewName(),
            new ModifierChangeGroup(),
            new ModifierGetNewGroup(),
            new ModifierUnknown(),
        };
        private readonly State _state = new State();

        public UserSession(string userID, IUsersRepository usersRepo, ISchedulesRepository schedulesRepo, State state)
        {
            usersRepository = usersRepo;
            schedulesRepository = schedulesRepo;

            Console.WriteLine($"Имя " + state.User.Name);
            _state.User = usersRepository.CreateUser(userID);

            Console.WriteLine($"Получена сессия пользователя {_state.User.ID}");
        }

        internal AliceResponse HandleRequest(AliceRequest aliceRequest, ref State state)
        {
            _state.Step = state.Step;
            AliceResponse response = null;
            if(!Modifiers.Any(modifier => modifier.Run(aliceRequest, _state, schedulesRepository,out response)))
            {
                throw new NotSupportedException("No default modifier");
            }

            _state.User = usersRepository.UpdateUser(_state.User);
            state = _state;

            return response;
        }
    }
}
