namespace Crystallum.View {
    public interface MainView {
        string getProgram();

        string showSaveProgramDialog();
        void showInvalidDimensionsError();

        void updateGeneratedProgram(string program);
    }
}
