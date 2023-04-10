using System;
using System.Text.RegularExpressions;
using System.Windows;
using TheBank2.ViewModel;

namespace TheBank2.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewUserWindow.xaml
    /// </summary>
    public partial class AddNewUserWindow : Window
    {
        public AddNewUserWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.UserDateOfBirth = DateTime.Now;
        }
        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
