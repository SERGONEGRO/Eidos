using System.Text.RegularExpressions;
using System.Windows;
using Eidos.Model;
using Eidos.ViewModel;

namespace Eidos.View
{
    /// <summary>
    /// Логика взаимодействия для EditPositionWindow.xaml
    /// </summary>
    public partial class EditPositionWindow : Window
    {
        public EditPositionWindow(Position<int> positionToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedPosition = positionToEdit;
            DataManageVM.PositionName = positionToEdit.Name;
            DataManageVM.PositionSalary = positionToEdit.Salary;
            DataManageVM.PositionDepartment = positionToEdit.PositionDepartment;
        }
        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
