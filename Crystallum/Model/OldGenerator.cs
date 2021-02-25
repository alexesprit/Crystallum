using System;
using System.Collections.Generic;
using System.Text;

using Crystallum.Error;
using Crystallum.Model.Shape;
using Crystallum.Util;

namespace Crystallum.Model
{
    class OldGenerator : IGenerator
    {
        string IGenerator.GenerateProgram(Rect rect)
        {
            if (!KristallParams.IsThicknessSupported(rect.thickness))
            {
                throw new InvalidThicknessException();
            }

            var moveX = KristallParams.Convert(rect.width + CUTTING_WIDTH);
            var moveY = KristallParams.Convert(rect.height + CUTTING_WIDTH);

            var startX = KristallParams.Convert(-(PADDING - START_RADIUS - CUTTING_WIDTH / 2));
            var startY = KristallParams.Convert(PADDING - START_RADIUS - CUTTING_WIDTH / 2);

            var radiusX = KristallParams.Convert(-START_RADIUS);
            var radiusY = KristallParams.Convert(START_RADIUS);
            var radiusI = KristallParams.Convert(START_RADIUS);
            var radiusJ = 0;

            var feed = KristallParams.GetFeed(rect.thickness);

            var replaceMap = new Dictionary<string, int>() {
                { "START_X", startX },
                { "START_Y", startY },
                { "RADIUS_X", radiusX },
                { "RADIUS_Y", radiusY },
                { "RADIUS_I", radiusI },
                { "RADIUS_J", radiusJ },
                { "MOVE_X",  moveX },
                { "MOVE_Y",  moveY },
                { "FEED",  feed },
             };

            var buf = new StringBuilder();
            buf.AppendLine(HEADER);
            buf.AppendLine(Helpers.FormatTemplate(RECT_TEMPLATE, replaceMap));
            buf.AppendLine(FOOTER);

            return buf.ToString();
        }

        string IGenerator.GenerateProgram(Circle circle)
        {
            if (!KristallParams.IsThicknessSupported(circle.thickness))
            {
                throw new InvalidThicknessException();
            }

            var diameter = 200;
            var radius = diameter / 2;

            var radiusCorr = radius + CUTTING_WIDTH / 2;

            var partCenterOffsetX = radius + PADDING;
            var partCenterOffsetY = partCenterOffsetX;

            var partStartX = radiusCorr / SQRT_2 - partCenterOffsetX;
            var partStartY = -radiusCorr / SQRT_2 + partCenterOffsetY;

            var startRadCenterX = (radiusCorr + START_RADIUS) / SQRT_2 - partCenterOffsetX;
            var startRadCenterY = -(radiusCorr + START_RADIUS) / SQRT_2 + partCenterOffsetY;

            var startX = START_RADIUS / SQRT_2 + startRadCenterX;
            var startY = START_RADIUS / SQRT_2 + startRadCenterY;

            var startXK = (int)(startX * KristallParams.DISCRETENESS);
            var startYK = (int)(startY * KristallParams.DISCRETENESS);

            var startIK = (int)((startRadCenterX - startX) * KristallParams.DISCRETENESS);
            var startJK = (int)((startRadCenterY - startY) * KristallParams.DISCRETENESS);

            var startXOffsetK = (int)((partStartX - startX) * KristallParams.DISCRETENESS);
            var startYOffsetK = (int)((partStartX - startY) * KristallParams.DISCRETENESS);

            var partEndX = radiusCorr / SQRT_2 * 2;
            var partEndY = radiusCorr / SQRT_2 * 2;
            var partEndI = partCenterOffsetX - partEndX;
            var partEndJ = partCenterOffsetY - partEndY;

            var partEndXK = KristallParams.Convert(partStartX - partEndX);
            var partEndYK = KristallParams.Convert(partEndY);
            var partEndIK = KristallParams.Convert(partEndI);
            var partEndJK = KristallParams.Convert(partEndJ);

            var replaceMap = new Dictionary<string, int>() {
                { "START_X", startXK },
                { "START_Y", startYK },
                { "START_I", startIK },
                { "START_J", startJK },
                { "START_X_OFFSET", startXOffsetK },
                { "START_Y_OFFSET", startYOffsetK },
                { "PART_END_X", partEndXK },
                { "PART_END_Y", partEndYK },
                { "PART_END_I", partEndIK },
                { "PART_END_J", partEndIK },
             };

            var buf = new StringBuilder();
            buf.Append(HEADER);
            buf.Append('\n');
            buf.Append(Helpers.FormatTemplate(CIRCLE_TEMPLATE, replaceMap));
            buf.Append('\n');
            buf.Append(FOOTER);

            return buf.ToString();
        }

        private const int PADDING = 15;
        private const int START_RADIUS = 8;
        private const int CUTTING_WIDTH = 4;
        private readonly double SQRT_2 = Math.Sqrt(2);

        private const string HEADER = @"%
                                        G91G40G01X0Y0F2000D50";

        private const string FOOTER = @"M83
                                        M2";

        private const string CIRCLE_TEMPLATE = @"G00X{START_X}Y{START_Y}
                                                 M81G04X20
                                                 G03X{START_X_OFFSET}I{START_I}J{START_J}F{FEED}
                                                 G02X{PART_END_X}Y{PART_END_Y}I{PART_END_I}J721
                                                 X1442Y-1442I721J-721";

        private const string RECT_TEMPLATE = @"G00X{START_X}Y{START_Y}
                                               M81G04X20
                                               G03X{RADIUS_X}Y{RADIUS_Y}I{RADIUS_I}J{RADIUS_J}F{FEED}
                                               G01X-{MOVE_X}
                                               Y{MOVE_Y}
                                               X{MOVE_X}
                                               Y-{MOVE_Y}";
    }
}
