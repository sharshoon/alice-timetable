using Alice_Timetable.Engine;

namespace alice_timetable.Engine.Modifiers.Helpers
{
    public class HelperAwaitForName : Helper
    {
        public override Step[] Steps => new Step[] { Step.AwaitForName, Step.AwaitForNewName };

        public override string GetHelper()
        {
            return "Скажите то, как вы хотите, чтобы я вас называла. Например: 'мой повелитель' ";
        }
    }
}
