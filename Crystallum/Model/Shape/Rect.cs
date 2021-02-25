using System;

namespace Crystallum.Model.Shape
{
    internal class Rect
    {
        private const int DIMENSIONS_COUNT = 3;

        internal int height;
        internal int width;
        internal int thickness;

        internal Rect(string dimensions)
        {
            var sizes = Util.Helpers.ExtractNumbers(dimensions);
            if (sizes.Length != DIMENSIONS_COUNT)
            {
                throw new FormatException();
            }

            width = sizes[0];
            height = sizes[1];
            thickness = sizes[2];
        }
    }
}
