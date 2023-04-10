using System.Windows;
using Eidos.ViewModel;

namespace Eidos.View
{
    /// <summary>
    /// Логика взаимодействия для AddDepartmentWindow.xaml
    /// </summary>
    public partial class AddNewDepartmentWindow : Window
    {
        public AddNewDepartmentWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
    }
}
