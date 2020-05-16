using Alice_Timetable.Engine;

namespace alice_timetable.Engine.Modifiers.Helpers
{
    public class HelperDisplayAuditory : Helper
    {
        public override Step[] Steps => new Step[] { Step.AwaitForDisplayingAuditory };

        public override string GetHelper()
        {
            return  "Выбери, буду ли я озвучивать аудиторию, в кототорой будет проходить занятие, или нет. " +
                    "Варианты ответа могут быть такими: 'да', 'нет', 'хочу', 'хотел бы', 'не хочу', 'не хотел бы', 'хорошо'";
        }
    }
}
