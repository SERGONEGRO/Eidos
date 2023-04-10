using System.Text.RegularExpressions;
using System.Windows;
using TheBank2.ViewModel;

namespace TheBank2.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewDepositWindow.xaml
    /// </summary>
    public partial class AddNewDepositWindow : Window
    {
        public AddNewDepositWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.DepositDateOfStart = System.DateTime.Now;
        }
        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
