using Microsoft.Win32;
using System.Windows;

using Crystallum.View;
using Crystallum.Properties;

namespace Crystallum {
    public partial class MainWindow : Window, MainView {
        private Presenter.MainPresenter presenter;

        public MainWindow() {
            InitializeComponent();
            UpdateComponents();

            presenter = new Presenter.MainPresenter(this);
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
            MessageBox.Show(
                Properties.Resources.InvalidDimensionsError,
                Properties.Resources.AppName,
                MessageBoxButton.OK
            );
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
        }
    }
}
