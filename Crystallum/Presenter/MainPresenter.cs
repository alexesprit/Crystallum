using System.IO;

using Crystallum.Error;
using Crystallum.Model;
using Crystallum.View;
using System;

namespace Crystallum.Presenter {
    public sealed class MainPresenter {
        private MainView view;

        private Generator generator;
        private Part part;

        public MainPresenter(MainView view) {
            this.view = view;
            generator = new Generator();
        }

        public void onDimensionsUpdate(string dimensionsStr) {
            part = null;
            try {
                part = Part.createFromString(dimensionsStr);
            } catch (CrystallumError) {
            }            
        }

        public void onGenerateButtonClicked() {
            if (part != null) {
                var program = generator.generateProgram(part);
                view.updateGeneratedProgram(program);
            } else {
                view.showInvalidDimensionsError();
            }
        }

        public void onSaveButtonClicked() {
            var pathToSave = view.showSaveProgramDialog();
            if (!String.IsNullOrEmpty(pathToSave)) {
                File.WriteAllText(pathToSave, view.getProgram());
            }
        }
    }
}
