using Microsoft.Win32;
using System.Windows;

using Crystallum.View;

namespace Crystallum {
    public partial class MainWindow : Window, MainView {
        private Presenter.MainPresenter presenter;

        public MainWindow() {
            presenter = new Presenter.MainPresenter(this);

            InitializeComponent();
            UpdateComponents();
        }

        public string getProgram() {
            return programTextBox.Text;
        }

        public string showSaveProgramDialog() {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "ISO G code|*.iso";
            dialog.Title = Properties.Resources.SaveGeneratedProgram;
            dialog.ShowDialog();

            return dialog.FileName;
        }

        public void showInvalidDimensionsError() {
            showError(Properties.Resources.InvalidDimensionsError);
        }

        public void showInvalidThicknessError() {
            showError("Invalid thickness");
        }

        public void updateGeneratedProgram(string program) {
            programTextBox.Text = program;
        }

        private void onGenerateProgramButtonClick(object sender, RoutedEventArgs e) {
            presenter.onDimensionsUpdate(dimensionsTextBox.Text);
            presenter.onGenerateButtonClicked();
        }

        private void onSaveButtonClick(object sender, RoutedEventArgs e) {
            presenter.onSaveButtonClicked(); 
        }

        private void UpdateComponents() {
            this.Title = Properties.Resources.AppName;

            rectRadioButton.Content = Properties.Resources.Rect;
            circleRadioButton.Content = Properties.Resources.Circle;
        }

        private void showError(string message) {
            MessageBox.Show(message, Properties.Resources.AppName, MessageBoxButton.OK);
        }

        private void onCircleRadioButtonChecked(object sender, RoutedEventArgs e) {
            presenter.onSelectCircleType();
            label.Content = Properties.Resources.CircleDimensions;
        }

        private void onRectRadioButtonChecked(object sender, RoutedEventArgs e) {
            presenter.onSelectRectType();
            label.Content = Properties.Resources.RectDimensions;
        }
    }
}
