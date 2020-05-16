using Alice_Timetable.Engine;

namespace alice_timetable.Engine.Modifiers.Helpers
{
    public class HelperAwaitForGroup : Helper
    {
        public override Step[] Steps => new Step[] { Step.AwaitForGroup, Step.AwaitForNewGroup };

        public override string GetHelper()
        {
            return "Тут нужно указать название своей учебной группы. Например: '851005'. Это нужно для того, " +
                "чтобы я потом каждый раз не спрашивала, каждый раз не спрашивала, какая у тебя группа, когда ты будешь просить расписание";
        }
    }
}
