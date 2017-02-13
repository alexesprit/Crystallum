using System;

using Crystallum.Error;

namespace Crystallum.Model {
    internal class Rect {
        private const int DIMENSIONS_COUNT = 3;

        internal int height;
        internal int width;
        internal int thickness;

        internal Rect(string dimensions) {
            var sizes = dimensions.Split('x');
            if (sizes.Length != DIMENSIONS_COUNT) {
                throw new CrystallumError();
            }

            width = Int32.Parse(sizes[0]);
            height = Int32.Parse(sizes[1]);
            thickness = Int32.Parse(sizes[2]);
        }
    }
}
