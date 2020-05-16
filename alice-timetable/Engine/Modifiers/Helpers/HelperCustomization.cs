using Alice_Timetable.Engine;

namespace alice_timetable.Engine.Modifiers.Helpers
{
    public class HelperCustomization : Helper
    {
        public override Step[] Steps => new Step[] { Step.AwaitForCustomizationAnswer };

        public override string GetHelper()
        {
            return "Эта опция сделана для того, чтобы ты мог более гибко настроить то, что я тебе озвучиваю. " +
                    "Я могу называть или не называть аудиторию, время, преподавателя и тип занятия. Если ты пропустишь этот шаг, " +
                    "то я не буду озвучивать ничего из этого, а только название предмета \n" +
                    "Ты можешь попробовать одну конфигурацию отображения, а потом, с помощью команды 'сменить параметры отображения', изменить значения \n" +
                    "Варианты ответа могут быть такими: 'да', 'нет', 'хочу', 'хотел бы', 'не хочу', 'не хотел бы', 'хорошо'";
        }
    }
}
