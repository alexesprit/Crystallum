using Crystallum.Util;
using System;
using System.Collections.Generic;

namespace Crystallum.Model {
    internal sealed class CircleGenerator {
        private const int PADDING = 20;
        private const int RADIUS = 15;

        internal string generateProgram(Circle part) {
            double radius = part.diameter / 2;

            double ax = PADDING;
            double ay = PADDING + radius;

            double bx = PADDING - RADIUS;
            double by = radius + PADDING - RADIUS;

            int AX = (int)((ax - bx) * KristallParams.DISCRETENESS);
            int AY = (int)((ay - by) * KristallParams.DISCRETENESS);

            int BX = (int)(bx * KristallParams.DISCRETENESS);
            int BY = (int)(by * KristallParams.DISCRETENESS);

            int CX = (int)(radius * KristallParams.DISCRETENESS);
            int CY = 0;

            var feed = KristallParams.FEED_MAP[part.thickness];

            var replaceMap = new Dictionary<string, int>() {
                { "{AX}", AX }, { "{AY}", AY },
                { "{BX}", BX }, { "{BY}", BY },
                { "{CX}", CX }, { "{CY}", CY },

                { "{RADIUS}",  RADIUS * KristallParams.DISCRETENESS },

                { "{FEED}",  feed },
                { "{HALFFEED}", feed / 2 }
             };

            return Formatter.formatTemplate(SHORT_PART_TEMPLATE, replaceMap);
        }

        private const string SHORT_PART_TEMPLATE = @"%
N5G91G40G01X0Y0F2000D50
N20G00X{BX}Y{BY}
N25M81G04X15
N30G03X{AX}Y{AY}I-{RADIUS}J0F{HALFFEED}
N35G02X0Y0I{CX}J{CY}F{FEED}
N40M83
N45M02
";
    }
}
