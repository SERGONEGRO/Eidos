using System.Text.RegularExpressions;
using System.Windows;
using TheBank2.ViewModel;

namespace TheBank2.View
{
    /// <summary>
    /// Логика взаимодействия для TransferDepositWindow.xaml
    /// </summary>
    public partial class TransferDepositWindow : Window
    {
        public TransferDepositWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
