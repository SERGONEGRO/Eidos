using System.Text.RegularExpressions;
using System.Windows;
using TheBank2.Model;
using TheBank2.ViewModel;
using MyLib;

namespace TheBank2.View
{
    /// <summary>
    /// Логика взаимодействия для EditClientWindow.xaml
    /// </summary>
    public partial class EditClientWindow : Window
    {
        public EditClientWindow(Client<int> clientToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedClient = clientToEdit;
            DataManageVM.ClientName = clientToEdit.Name;
            DataManageVM.ClientSurName = clientToEdit.SurName;
            DataManageVM.ClientPhone = clientToEdit.Phone;
            DataManageVM.ClientDateOfBirth = clientToEdit.DateOfBirth;
            DataManageVM.ClientIsVIP = clientToEdit.IsVIP;
        }
        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
