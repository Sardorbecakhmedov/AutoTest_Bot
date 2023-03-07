using AutoTest.Models.Questions;
using Newtonsoft.Json;


namespace AutoTest.Services.ForQuestionsServices
{
    partial class QuestionsServices
    {
        public List<QuestionModel>? Questions { get; set; }
        public QuestionsServices()
        {
            string filePath = @"uzlotin.json";

            var json = File.ReadAllText(filePath);
            Questions = JsonConvert.DeserializeObject<List<QuestionModel>>(json) ?? new List<QuestionModel>();
        }

        public static List<(string, string)> GetListText(int index)
        {
            var list =  new List<List<(string, string)>>()
            {
                GetTextForButton1(),
                GetTextForButton2(),
                GetTextForButton3(),
                GetTextForButton4()
            };

            if (index > list.Count)
                return null!;

            return list[index];
        }

        public static List<(string, string)> GetTextForButton1()
        {
            return new List<(string, string)>()
            {
                ("1", "1-10"),
                ("2", "11-20"),
                ("3", "21-30"),
                ("4", "31-40"),
                ("5", "41-50"),
                ("6", "51-60"),
                ("7", "61-70"),
                ("8", "71-80"),
                ("9", "81-90"),
                ("10", "91-100"),
                ("11", "101-110"),
                ("12", "111-120"),
                ("13", "121-130"),
                ("14", "131-140"),
                ("15", "141-150"),
                ("16", "151-160"),
                ("17", "161-170"),
                ("18", "171-180"),
            };
        }
        public static List<(string, string)> GetTextForButton2()
        {
            return new List<(string, string)>()
            {
                ("19", "181-190"),
                ("20", "191-200"),
                ("21", "201-210"),
                ("22", "210-220"),
                ("23", "221-230"),
                ("24", "231-240"),
                ("25", "241-250"),
                ("26", "251-260"),
                ("27", "261-270"),
                ("28", "271-280"),
                ("29", "271-290"),
                ("30", "281-300"),
                ("31", "291-310"),
                ("32", "301-320"),
                ("33", "321-330"),
                ("34", "331-340"),
                ("35", "341-350"),
                ("36", "351-360"),
            };
        }
        public static List<(string, string)> GetTextForButton3()
        {
            return new List<(string, string)>()
            {
                ("37", "361-370"),
                ("38", "371-380"),
                ("39", "381-390"),
                ("40", "391-400"),
                ("41", "401-410"),
                ("42", "411-420"),
                ("43", "421-430"),
                ("44", "431-440"),
                ("45", "441-450"),
                ("46", "451-460"),
                ("47", "461-470"),
                ("48", "471-480"),
                ("49", "481-490"),
                ("50", "491-500"),
                ("51", "501-510"),
                ("52", "511-520"),
                ("53", "521-530"),
                ("54", "531-540")
            };
        }
        public static List<(string, string)> GetTextForButton4()
        {
            return new List<(string, string)>()
            {
                ("55", "541-550"),
                ("56", "551-560"),
                ("57", "561-570"),
                ("58", "571-580"),
                ("59", "581-590"),
                ("60", "591-600"),
                ("61", "601-610"),
                ("62", "611-620"),
                ("63", "621-630"),
                ("64", "631-640"),
                ("65", "641-650"),
                ("66", "651-660"),
                ("67", "661-670"),
                ("68", "671-680"),
                ("69", "681-690"),
                ("70", "691-700"),
                (" ", " "),
                (" ", " ")
            };
        }

    }
}
