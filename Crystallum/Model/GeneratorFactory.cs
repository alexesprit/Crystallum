using Crystallum.Error;

namespace Crystallum.Model {
    public sealed class GeneratorFactory {
        public enum TYPE { RECT, CIRCLE };

        public string Dimensions;
        public TYPE Type;

        private CircleGenerator circleGenerator;
        private RectGenerator rectGenerator;

        public GeneratorFactory() {
            circleGenerator = new CircleGenerator();
            rectGenerator = new RectGenerator();
        }

        public string generateProgram() {
            switch (Type) {
                case TYPE.RECT:
                    var rect = new Rect(Dimensions);
                    return rectGenerator.generateProgram(rect);
                case TYPE.CIRCLE:
                    var circle = new Circle(Dimensions);
                    return circleGenerator.generateProgram(circle);
                default:
                    throw new CrystallumError();
            }
        }
    }
}
