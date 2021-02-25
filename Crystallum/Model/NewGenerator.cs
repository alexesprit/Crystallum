using System;
using Crystallum.Model.Shape;

namespace Crystallum.Model
{
    class NewGenerator : IGenerator
    {
        string IGenerator.GenerateProgram(Rect rect)
        {
            return "new rect";
        }

        string IGenerator.GenerateProgram(Circle circle)
        {
            return "new circle";
        }
    }
}
