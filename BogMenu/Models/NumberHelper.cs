using System;
using System.Collections.Generic;
using System.Linq;
namespace BogMenu.Models
{
    public class NumberHelper
    {
        private static Dictionary<string, string> data = new Dictionary<string, string>(){{ "0", "ნული"}, { "1", "ერთი"}, { "2", "ორი"}, { "3", "სამი"},
            { "4", "ოთხი"},{"5", "ხუთი" },{"6", "ექვსი" },{"7", "შვიდი"}, {"8", "რვა"}, {"9", "ცხრა"},
            { "10", "ათი"}, { "11", "თერთმეტი"}, { "12", "თორმეტი"}, { "13", "ცამეტი"}, {"14", "თოთხმეტი"},
            {"15", "თხუთმეტი" },{"16", "თექვსმეტი" },{"17", "ჩვიდმეტი"}, {"18", "თვრამეტი" }, {"19", "ცხრამეტი"},
            {"2*", "ოც" },{ "3*", "ოც"}, {"4*", "ორმოც"}, {"5*", "ორმოც"}, {"6*", "სამოც"}, {"7*", "სამოც"},
            {"8*", "ოთხმოც"}, {"9*", "ოთხმოც"}};
        private static Dictionary<int, string> units = new Dictionary<int, string>() { { 2, "ას" }, { 3, "ათას" }, { 6, "მილიონ" }, { 9, "მილიარდ" } };

        public static string NumberToString(int n)
        {
            string result = "";
            string number = n.ToString();
            if (data.Keys.Contains(number)) return data[number];
            string f = number.Substring(0, 1);
            string s = number.Substring(1);
            if (f.Equals("-")) return "მინუს " + NumberToString(Math.Abs(n));
            if (number.Length == 2)
                result = data[f + "*"] + (s.Equals("0") ?
                    ((n / 10) % 2 == 1 ? "დაათი" : "ი") : ("და" + ((n / 10) % 2 == 1 ? data["1" + s] : data[s])));
            if (number.Length == 3)
                result = (f.Equals("1") ? "" : ((f.Equals("8") || f.Equals("9")) ?
                            data[f] : data[f].Substring(0, data[f].Length - 1))) + units[2] + (n % 100 == 0 ? "ი" : " " + NumberToString(n % 100));
            if (number.Length > 3)
            {
                int unit = ((number.Length - 1) / 3) * 3;
                int unitValue = (int)Math.Pow(10, unit);
                result = ((f.Equals("1") && (number.Length - 1) % 3 == 0) ? "" : NumberToString(n / unitValue) + " ") + units[unit] + (n % unitValue == 0 ? "ი" : " " + NumberToString(n % unitValue));
            }
            return result;
        }

        public static string MoneyToString(string input)
        {
            string result = "";
            int point = input.IndexOf('.');
            result = ((point < 0) ? NumberToString(Convert.ToInt32(input)) + " ლარი" :
              NumberToString(Convert.ToInt32(input.Substring(0, point))) + " ლარი და " +
                ((input.Substring(point + 1).Length == 2 || input.Substring(point + 1)[0] == '0') ?
                  NumberToString(Convert.ToInt32(input.Substring(point + 1))) :
                    NumberToString(Convert.ToInt32(input.Substring(point + 1)) * 10)) + " თეთრი");
            return result;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                string input;
                try
                {
                    input = Console.ReadLine();
                }
                catch
                {
                    break;
                }
                Console.WriteLine(MoneyToString(input));
            }
            Console.ReadKey();
        }
    }
}