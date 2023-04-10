using System.Text.RegularExpressions;
using System.Windows;
using TheBank2.Model;
using TheBank2.ViewModel;
namespace TheBank2.View
{
    /// <summary>
    /// Логика взаимодействия для EditDepositWindow.xaml
    /// </summary>
    public partial class EditDepositWindow : Window
    {
        public EditDepositWindow(Deposit<int> depositToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedDeposit = depositToEdit;
            DataManageVM.DepositClient = depositToEdit.DepositClient;
            DataManageVM.DepositStartSum = depositToEdit.StartSum;
            DataManageVM.DepositPercent = depositToEdit.DepositPercent;
            DataManageVM.DepositIsCapitalized = depositToEdit.IsCapitalized;
            DataManageVM.DepositDateOfStart = depositToEdit.DateOfStart;
            DataManageVM.DepositMonthsCount = depositToEdit.MonthsCount;
            DataManageVM.DepositResponsibleEmployee = depositToEdit.ResponsibleEmployee;
        }
        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
