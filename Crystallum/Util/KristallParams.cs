using System.Collections.Generic;

namespace Crystallum.Util {
    public sealed class KristallParams {
        public const int DISCRETENESS = 10;

        public static Dictionary<int, int> FEED_MAP = new Dictionary<int, int>() {
            { 6, 2200 },
            { 8, 2200 },
            { 10, 2000 },
            { 12, 1500 },
            { 14, 1400 },
            { 30, 675 }
        };
    }
}
