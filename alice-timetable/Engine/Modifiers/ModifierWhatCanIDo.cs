using Alice_Timetable.Engine;
using Alice_Timetable.Engine.Modifiers;
using Alice_Timetable.Models;
using System.Collections.Generic;
using System.Linq;

namespace alice_timetable.Engine.Modifiers
{
    public class ModifierWhatCanIDo : ModifierBase
    {
        protected override bool Check(AliceRequest request, State state)
        {
            var keywords = new List<string>
            {
                "��� �� ������",
                "��� �� ������",
                "��� ������������"
            };

            var requestString = request.Request.Nlu.Tokens;
            return keywords.Any(kw =>
            {
                var tokens = kw.Split(" ");
                return tokens.All(requestString.ContainsStartWith);
            });

        }

        protected override SimpleResponse Respond(AliceRequest request, ISchedulesRepository schedulesRepo, State state)
        {
            return new SimpleResponse()
            { 
                Text =  $"� ���� �������� ���� ����������� ���������� ����� ������. \n " +
                        $"��� ����� �� ������ ������������ ����� ������� ��� '���������� �� ������', '���������� �� �������', '���������� �� 26.05' � ��� ����� \n" +
                        $"� ���� �������� ���� ���� ���������� �������� ����� �������������. ��������, ���� �� ������ � ���� ���������, ��� �� ��������� " +
                        $"����� ���������, ������������ �������, ��� ������� � ����� ��� ����������. ��� ��� �� ������������ ��� ������ �����, " +
                        $"� ��� �� ������ �������� ��, ������ '������� ��������� �����������'. \n" +
                        $"����� ����, �� ��� �� ������ ������ ��, ��� � ���� �������, ������ '������� ���' � �������� ����� ����� ������, ������ '������� ������' \n" +
                        $"����� ����������� ���������� ������, � ��� �� ���� ���������� ���������� ������-�� ����������� �������������. ��������: " +
                        $"'���������� ������������ ������ ������ �� �������' ��� '���������� � ������� �� �������' \n" +
                        $"���� ���� ����� ���-�� ���������, �� �� ������ ����� ���� �� ������ ������� '������' ��� '������ ���' � � ��������, ��� ���� �����������"
            };
        }
    }
}