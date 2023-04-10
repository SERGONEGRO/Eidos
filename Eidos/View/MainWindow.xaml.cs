using System.Windows;
using System.Windows.Controls;
using TheBank2.ViewModel;

namespace TheBank2.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllDepartmentsView;
        public static ListView AllPositionsView;
        public static ListView AllUsersView;
        public static ListView AllClientsView;
        public static ListView AllDepositsView;
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            AllDepartmentsView = ViewAllDepartments;
            AllPositionsView = ViewAllPositions;
            AllUsersView = ViewAllUsers;
            AllClientsView = ViewAllClients;
            AllDepositsView = ViewAllDeposits;
        }

       
    }
}
