using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crystallum.Util {
    public sealed class Helpers {
        private static char[] separators = { 'x', 'х' };

        public static int[] ExtractNumbers(string text) {
            if (text == null || text.Length == 0) {
                return null;
            }

            var rawNumbers = text.Split(separators);
            var numbers = new List<int>();

            foreach (var rawNumber in rawNumbers) {
                try {
                    int number = int.Parse(rawNumber.Trim());
                    numbers.Add(number);
                } catch {
                    continue;
                }
            }

            return numbers.ToArray();
        }
    }
}
