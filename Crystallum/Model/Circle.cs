using System;

namespace Crystallum.Model {
    internal class Circle {
        private const int DIMENSIONS_COUNT = 2;

        internal int diameter;
        internal int thickness;

        internal Circle(string dimensions) {
            var sizes = Util.Helpers.ExtractNumbers(dimensions);
            if (sizes.Length != DIMENSIONS_COUNT) {
                throw new FormatException();
            }

            diameter = sizes[0];
            thickness = sizes[1];
        }
    }
}
