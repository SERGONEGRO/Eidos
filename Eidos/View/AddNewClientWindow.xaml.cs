using System.Text.RegularExpressions;
using System.Windows;
using TheBank2.ViewModel;

namespace TheBank2.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewClient.xaml
    /// </summary>
    public partial class AddNewClientWindow : Window
    {
        public AddNewClientWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.ClientDateOfBirth = System.DateTime.Now;
        }
        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
