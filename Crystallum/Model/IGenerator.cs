using Crystallum.Model.Shape;

namespace Crystallum.Model {
    interface IGenerator {
        string GenerateProgram(Rect rect);
        string GenerateProgram(Circle circle);
    }
}
