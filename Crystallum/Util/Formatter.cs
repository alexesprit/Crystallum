using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crystallum.Util {
    public sealed class Formatter {
        public static string formatTemplate(string template, Dictionary<string, int> replaceMap) {
            foreach (var item in replaceMap) {
                template = template.Replace(item.Key, item.Value.ToString());
            }

            return template;
        }
    }
}
