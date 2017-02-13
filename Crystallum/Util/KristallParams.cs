using System.Collections.Generic;

namespace Crystallum.Util {
    public sealed class KristallParams {
        public const int DISCRETENESS = 10;

        public static Dictionary<int, int> FEED_MAP = new Dictionary<int, int>() {
            { 8, 2200 },
            { 10, 2000 }
        };
    }
}
