namespace Crystallum.View {
    public interface MainView {
        string GetProgram();

        string ShowSaveProgramDialog();

        void ShowInvalidDimensionsError();
        void ShowInvalidThicknessError();

        void UpdateGeneratedProgram(string program);
    }
}
