using Alice_Timetable.Engine;

namespace alice_timetable.Engine.Modifiers.Helpers
{
    public abstract class Helper
    {
        public abstract string GetHelper();
        public abstract Step[] Steps { get; }
    }
}
