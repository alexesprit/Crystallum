using System.IO;

using Crystallum.Model;
using Crystallum.View;
using System;

namespace Crystallum.Presenter {
    public sealed class MainPresenter {
        private MainView view;

        private GeneratorFactory generator;

        public MainPresenter(MainView view) {
            this.view = view;
            generator = new GeneratorFactory();
        }

        public void onDimensionsUpdate(string dimensionsStr) {
            generator.Dimensions = dimensionsStr;
        }

        public void onGenerateButtonClicked() {
            try {
                var program = generator.generateProgram();
                view.updateGeneratedProgram(program);
            } catch (FormatException) {
                view.showInvalidDimensionsError();
            }
        }

        public void onSaveButtonClicked() {
            var pathToSave = view.showSaveProgramDialog();
            if (!String.IsNullOrEmpty(pathToSave)) {
                File.WriteAllText(pathToSave, view.getProgram());
            }
        }

        public void onSelectRectType() {
            generator.Type = GeneratorFactory.TYPE.RECT;
        }

        public void onSelectCircleType() {
            generator.Type = GeneratorFactory.TYPE.CIRCLE;
        }
    }
}
