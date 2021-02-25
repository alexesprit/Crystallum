using System.Collections.Generic;
using System.Text;

namespace Crystallum.Util
{
    using ReplaceMap = Dictionary<string, int>;

    public sealed class Helpers
    {
        private static readonly char[] separators = { 'x', 'х' };

        public static int[] ExtractNumbers(string text)
        {
            if (text == null || text.Length == 0)
            {
                return null;
            }

            var rawNumbers = text.Split(separators);
            var numbers = new List<int>();

            foreach (var rawNumber in rawNumbers)
            {
                try
                {
                    int number = int.Parse(rawNumber.Trim());
                    numbers.Add(number);
                }
                catch
                {
                    continue;
                }
            }

            return numbers.ToArray();
        }

        public static string FormatTemplate(string template, ReplaceMap replaceMap)
        {
            foreach (var item in replaceMap)
            {
                template = template.Replace($"{{{item.Key}}}", item.Value.ToString());
            }

            return template;
        }

        public static string Enumerate(string program)
        {
            var lines = program.Trim().Split('\n');
            var counter = 1;
            var buf = new StringBuilder();
            
            foreach (var line in lines)
            {
                var newLine = line.Trim();
                if (newLine != "%")
                {
                    newLine = $"N{counter}{newLine}";
                    ++counter;
                }

                buf.AppendLine(newLine);
            }

            return buf.ToString();
        }
    }
}
