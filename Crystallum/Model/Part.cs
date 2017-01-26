using System;

using Crystallum.Error;

namespace Crystallum.Model {
    public class Part {
        public int height;
        public int width;
        public int thickness;

        private Part(int width, int height, int thickness) {
            this.width = width;
            this.height = height;
            this.thickness = thickness;
        }

        public static Part createFromString(string str) {
            var sizes = str.Split('x');
            if (sizes.Length != 3) {
                throw new CrystallumError();
            }

            int width = Int32.Parse(sizes[0]);
            int height = Int32.Parse(sizes[1]);
            int thickness = Int32.Parse(sizes[2]);
            
            return new Part(width, height, thickness);
        }
    }
}
