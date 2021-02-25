using Crystallum.Error;
using Crystallum.Model.Shape;
using Crystallum.Util;

namespace Crystallum.Model
{
    public sealed class GeneratorFactory
    {
        public enum Shape { RECT, CIRCLE };
        public enum Machine { NEW, OLD };

        public string Dimensions;
        public Shape ShapeType;
        public Machine MachineType;

        private readonly NewGenerator newGenerator;
        private readonly OldGenerator oldGenerator;

        public GeneratorFactory()
        {
            newGenerator = new NewGenerator();
            oldGenerator = new OldGenerator();
        }

        public string GenerateProgram()
        {
            var rawProgram = GetRawProgram();
            return Helpers.Enumerate(rawProgram);
        }

        private string GetRawProgram()
        {
            var generator = GetCurrentGenerator();

            switch (ShapeType)
            {
                case Shape.RECT:
                    var rect = new Rect(Dimensions);
                    return generator.GenerateProgram(rect);
                case Shape.CIRCLE:
                    var circle = new Circle(Dimensions);
                    return generator.GenerateProgram(circle);
                default:
                    throw new CrystallumError();
            }
        }

        private IGenerator GetCurrentGenerator()
        {
            switch (MachineType)
            {
                case Machine.NEW:
                    return newGenerator;
                case Machine.OLD:
                    return oldGenerator;
                default:
                    throw new CrystallumError();
            }
        }
    }
}
