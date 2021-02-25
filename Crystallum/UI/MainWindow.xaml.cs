using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

using Crystallum.View;
using Crystallum.Presenter;

namespace Crystallum
{
    public partial class MainWindow : Window, MainView
    {
        private readonly MainPresenter presenter;

        public MainWindow()
        {
            presenter = new MainPresenter(this);

            InitializeComponent();
            UpdateComponents();
        }

        public string GetProgram()
        {
            return programTextBox.Text;
        }

        public string ShowSaveProgramDialog()
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "ISO G code|*.iso",
                Title = Properties.Resources.SaveGeneratedProgram
            };
            dialog.ShowDialog();

            return dialog.FileName;
        }

        public void ShowInvalidDimensionsError()
        {
            ShowError(Properties.Resources.InvalidDimensionsError);
        }

        public void ShowInvalidThicknessError()
        {
            ShowError("Invalid thickness");
        }

        public void UpdateGeneratedProgram(string program)
        {
            programTextBox.Text = program;
        }

        private void OnGenerateProgramButtonClick(object sender, RoutedEventArgs e)
        {
            presenter.onDimensionsUpdate(dimensionsTextBox.Text);
            presenter.onGenerateButtonClicked();
        }

        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            presenter.onSaveButtonClicked();
        }

        private void UpdateComponents()
        {
            this.Title = Properties.Resources.AppName;

            rectRadioButton.Content = Properties.Resources.Rect;
            circleRadioButton.Content = Properties.Resources.Circle;
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, Properties.Resources.AppName, MessageBoxButton.OK);
        }

        private void OnCircleRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            presenter.onSelectCircleType();
            label.Content = Properties.Resources.CircleDimensions;
        }

        private void OnRectRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            presenter.onSelectRectType();
            label.Content = Properties.Resources.RectDimensions;
        }


        private void OnNewRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            presenter.OnSelectNewMachine();
        }

        private void OnOldRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            presenter.OnSelectOldMachine();
        }

        private void OnDimensionsTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                presenter.onDimensionsUpdate(dimensionsTextBox.Text);
                presenter.onGenerateButtonClicked();
            }
        }
    }
}
