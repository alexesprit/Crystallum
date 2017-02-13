using Crystallum.Error;
using System;

namespace Crystallum.Model {
    internal class Circle {
        private const int DIMENSIONS_COUNT = 2;

        internal int diameter;
        internal int thickness;

        internal Circle(string dimensions) {
            var sizes = dimensions.Split('x');
            if (sizes.Length != DIMENSIONS_COUNT) {
                throw new FormatException();
            }

            diameter = Int32.Parse(sizes[0]);
            thickness = Int32.Parse(sizes[1]);
        }
    }
}
