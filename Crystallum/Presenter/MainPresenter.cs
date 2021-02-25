using System.IO;

using Crystallum.Model;
using Crystallum.View;
using System;
using Crystallum.Error;

namespace Crystallum.Presenter
{
    public sealed class MainPresenter
    {
        private readonly MainView view;
        private readonly GeneratorFactory generator;

        public MainPresenter(MainView view)
        {
            this.view = view;
            generator = new GeneratorFactory();
        }

        public void onDimensionsUpdate(string dimensionsStr)
        {
            generator.Dimensions = dimensionsStr;
        }

        public void onGenerateButtonClicked()
        {
            try
            {
                var program = generator.GenerateProgram();
                view.UpdateGeneratedProgram(program);
            }
            catch (FormatException)
            {
                view.ShowInvalidDimensionsError();
            }
            catch (InvalidThicknessException)
            {
                view.ShowInvalidThicknessError();
            }
        }

        public void onSaveButtonClicked()
        {
            var pathToSave = view.ShowSaveProgramDialog();
            if (!string.IsNullOrEmpty(pathToSave))
            {
                File.WriteAllText(pathToSave, view.GetProgram());
            }
        }

        public void onSelectRectType()
        {
            generator.ShapeType = GeneratorFactory.Shape.RECT;
        }

        public void onSelectCircleType()
        {
            generator.ShapeType = GeneratorFactory.Shape.CIRCLE;
        }

        public void OnSelectNewMachine()
        {
            generator.MachineType = GeneratorFactory.Machine.NEW;
        }

        public void OnSelectOldMachine()
        {
            generator.MachineType = GeneratorFactory.Machine.OLD;
        }
    }
}
