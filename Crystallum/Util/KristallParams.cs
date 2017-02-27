using System.Collections.Generic;

namespace Crystallum.Util {
    public sealed class KristallParams {
        public const int DISCRETENESS = 10;

        private static Dictionary<int, int> feedMap = new Dictionary<int, int>() {
            { 6, 2200 },
            { 8, 2200 },
            { 10, 2000 },
            { 12, 1500 },
            { 14, 1400 },
            { 30, 675 }
        };

        public static int getFeed(int thickness) {
            return feedMap[thickness];
        }

        public static bool isSupported(int thickness) {
            return feedMap.ContainsKey(thickness);
        }
    }
}
