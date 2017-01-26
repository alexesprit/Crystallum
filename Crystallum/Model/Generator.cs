using Crystallum.Util;
using System.Collections.Generic;

namespace Crystallum.Model {
    public sealed class Generator {
        private static Dictionary<int, int> FEED_MAP = new Dictionary<int, int>() {
            { 8, 2200 },
            { 10, 2000 }
        };

        private static Dictionary<int, int> WIDTH_MAP = new Dictionary<int, int>() {
            { 4300, 1300 },
            { 4700, 1400 },
            { 5000, 1500 },
            { 5400, 1600 },
            { 5700, 1700 },
            { 6000, 1800 }
        };
        private const int MAX_SIZE_1 = 1800;
        private const int TAB_SIZE = 20;

        private const int DISCRETENESS = 10;

        public string generateProgram(Part part) {
            var feed = FEED_MAP[part.thickness];
            var replaceMap = new Dictionary<string, int>() {
                { "${width}",  part.width * DISCRETENESS },
                { "${height}", part.height * DISCRETENESS },
                { "${feed}",  feed },
                { "${feed2}", feed / 2 }
            };

            var template = getTemplate(part);
            return Formatter.formatTemplate(template, replaceMap);
        }

        public static string getTemplate(Part part) {
            string template;

            if (part.width <= 2500) {
                template = getTemplateOfShortPart();
            } else if (part.width <= 4000) {
                template = getMediumTemplate(part);
            } else {
                template = getLongTemplate(part);
            }

            return template;
        }

        private static string getTemplateOfShortPart() {
            return SHORT_PART_TEMPLATE;
        }

        private static string getMediumTemplate(Part part) {
            var size1 = (part.width - TAB_SIZE) / 2;
            var size2 = part.width - size1 - TAB_SIZE;

            var replaceMap = new Dictionary<string, int>() {
                 { "${size1}", size1 * DISCRETENESS },
                 { "${size2}", size2 * DISCRETENESS }
             };

            return Formatter.formatTemplate(MEDIUM_PART_TEMPLATE, replaceMap);
        }

        private static string getLongTemplate(Part part) {
            var size1 = 0;
            foreach(var item in WIDTH_MAP) {
                var maxWidth = item.Key;
                if (part.width <= maxWidth) {
                    size1 = item.Value;
                }
            }

            if (size1 == 0) {
                size1 = MAX_SIZE_1;
            }

            var size2 = part.width - size1 * 2 - TAB_SIZE * 2;

            var replaceMap = new Dictionary<string, int>() {
                 { "${size1}", size1 * DISCRETENESS },
                 { "${size2}", size2 * DISCRETENESS }
             };

            return Formatter.formatTemplate(LONG_PART_TEMPLATE, replaceMap);
        }

        private const string LONG_PART_TEMPLATE = @"%
N15G91G40G01X0Y0F2000D50
N20G00X50Y30
N25M81G04X15
N30G02X150Y150I150J0F${feed2}
N35G01X${size1}F${feed}
N40M83
N55G00X200
N60M81G04X15
N65G01X${size2}
N70M83
N75G00X200
N80M81 G04X15
N85G01X${size1}
N90G03X20Y20I0J20
N95G01Y${height}
N100G03X-20Y20I-20J0
N105G01X-${size1}
N110M83
N115G00X-200
N120M81G04X15
N125G01X-${size2}
N130M83
N135G00X-200
N140M81G04X15
N145G01X-${size1}
N150G03X-20Y-20I0J-20
N155G01Y-${height}
N160M83
N165M02
";

        private const string MEDIUM_PART_TEMPLATE = @"%
N15G91G40G01X0Y0F2000D50
N20G00X50Y30
N25M81G04X15
N30G02X150Y150I150J0F${feed2}
N35G01X${size1}F${feed}
N40M83
N55G00X200
N60M81G04X15
N65G01X${size2}
N70G03X20Y20I0J20
N75G01Y${height}
N80G03X-20Y20I-20J0
N85G01X-${size2}
N90M83
N95G00X-200
N100M81G04X15
N105G01X-${size1}
N110G03X-20Y-20I0J-20
N115G01Y-${height}
N120M83
N125M02
";

        private const string SHORT_PART_TEMPLATE = @"%
N15G91G40G01X0Y0F2000D50
N20G00X50Y30
N25M81G04X15
N30G02X150Y150I150J0F${feed2}
N35G01X${width}F${feed}
N40G03X20Y20I0J20
N45G01Y${height}
N50G03X-20Y20I-20J0
N55G01X-${width}
N60G03X-20Y-20I0J-20
N65G01Y-${height}
N70G03X20Y-20I20J0
N75M83
N80M02
";
    }
}
