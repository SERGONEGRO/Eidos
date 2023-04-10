using TheBank2.Model;
using TheBank2.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using MyLib;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace TheBank2.ViewModel
{
    internal class DataManageVM : INotifyPropertyChanged
    {


        #region СВОЙСТВА
        #region СВОЙСТВА ДЛЯ ДЕПАРТАМЕНТА
        public static string DepartmentName { get; set; }
        #endregion

        #region СВОЙСТВА ДЛЯ ПОЗИЦИИ
        public static string PositionName { get; set; }
        public static decimal PositionSalary { get; set; }
        public static int PositionMaxNumber { get; set; }
        public static Department<int> PositionDepartment { get; set; }
        #endregion

        #region СВОЙСТВА ДЛЯ ЮЗЕРА
        public static string UserName { get; set; }
        public static string UserSurName { get; set; }
        public static string UserPhone { get; set; }
        public static DateTime UserDateOfBirth { get; set; }
        public static Position<int> UserPosition { get; set; }
        #endregion

        #region СВОЙСТВА ДЛЯ КЛИЕНТА
        public static string ClientName { get; set; }
        public static string ClientSurName { get; set; }
        public static string ClientPhone { get; set; }
        public static DateTime ClientDateOfBirth { get; set; }
        public static bool ClientIsVIP { get; set; }
        #endregion

        #region СВОЙСТВА ДЛЯ ДЕПОЗИТА
        public static Client<int> DepositClient { get; set; }
        public static double DepositPercent { get; set; }
        public static int DepositStartSum { get; set; }
        public static bool DepositIsCapitalized { get; set; }
        public static User<int> DepositResponsibleEmployee { get; set; }
        public static DateTime DepositDateOfStart { get; set; }
        public static int DepositMonthsCount { get; set; }
        public static Deposit<int> DestinationDeposit { get; set; }
        public static int DepositSumToTransfer { get; set; }
        #endregion

        #region СВОЙСТВА ДЛЯ ВЫДЕЛЕННЫХ ЭЛЕМЕНТОВ

        public TabItem SelectedTabItem { get; set; }
        public static Position<int> SelectedPosition { get; set; }
        public static User<int> SelectedUser { get; set; }
        public static Department<int> SelectedDepartment { get; set; }
        public static Client<int> SelectedClient { get; set; }
        public static Deposit<int> SelectedDeposit { get; set; }
        #endregion

        /// <summary>
        /// все отделы
        /// </summary>
        private List<Department<int>> allDepartments = DataWorker.GetAllDepartments();
        public List<Department<int>> AllDepartments
        {
            get => allDepartments;
            set
            {
                allDepartments = value;
                NotifyPropertyChanged(nameof(AllDepartments));
            }
        }

        /// <summary>
        /// все позиции
        /// </summary>
        private List<Position<int>> allPositions = DataWorker.GetAllPositions();
        public List<Position<int>> AllPositions
        {
            get => allPositions;
            set
            {
                allPositions = value;
                NotifyPropertyChanged(nameof(AllPositions));
            }
        }

        /// <summary>
        /// все юзеры
        /// </summary>
        private List<User<int>> allUsers = DataWorker.GetAllUsers();
        public List<User<int>> AllUsers
        {
            get => allUsers;
            set
            {
                allUsers = value;
                NotifyPropertyChanged(nameof(AllUsers));
            }
        }

        /// <summary>
        /// все клиенты
        /// </summary>
        private List<Client<int>> allClients = DataWorker.GetAllClients();
        public List<Client<int>> AllClients
        {
            get => allClients;
            set
            {
                allClients = value;
                NotifyPropertyChanged(nameof(AllClients));
            }
        }

        /// <summary>
        /// все депозиты
        /// </summary>
        private List<Deposit<int>> allDeposits = DataWorker.GetAllDeposits();
        public List<Deposit<int>> AllDeposits
        {
            get => allDeposits;
            set
            {
                allDeposits = value;
                NotifyPropertyChanged(nameof(AllDeposits));
            }
        }

        /// <summary>
        /// Cобытие "свойство изменилось"
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Обработчик события "свойство изменилось"
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region МЕТОДЫ ОТКРЫТИЯ ОКОН ДЛЯ ДОБАВЛЕНИЯ
        /// <summary>
        /// Открытие окна добавления департмента
        /// </summary>
        private static void OpenAddDepartmentWindowMethod()
        {
            AddNewDepartmentWindow newDepartmentWindow = new();
            SetCenterPositionAndOpen(newDepartmentWindow);
        }

        /// <summary>
        /// Открытие окна добавления позиции
        /// </summary>
        private static void OpenAddPositionWindowMethod()
        {
            AddNewPositionWindow newPositionWindow = new();
            SetCenterPositionAndOpen(newPositionWindow);
        }

        /// <summary>
        /// Открытие окна добавления пользователя
        /// </summary>
        private static void OpenAddUserWindowMethod()
        {
            AddNewUserWindow newUserWindow = new();
            SetCenterPositionAndOpen(newUserWindow);
        }

        /// <summary>
        /// Открытие окна добавления клиента
        /// </summary>
        private static void OpenAddClientWindowMethod()
        {
            AddNewClientWindow newClientWindow = new();
            SetCenterPositionAndOpen(newClientWindow);
        }

        /// <summary>
        /// Открытие окна добавления депозита
        /// </summary>
        private static void OpenAddDepositWindowMethod()
        {
            AddNewDepositWindow newDepositWindow = new();
            SetCenterPositionAndOpen(newDepositWindow);
        }

        /// <summary>
        /// Открытие окна создания перевода
        /// </summary>
        private static void OpenTransferDepositWindowMethod()
        {
            TransferDepositWindow newTransferDepositWindow = new();
            SetCenterPositionAndOpen(newTransferDepositWindow);
        }

        /// <summary>
        /// Открывает окно в центре предыдущего окна
        /// </summary>
        /// <param name="window"></param>
        private static void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

        #region МЕТОДЫ ОТКРЫТИЯ ОКОН ДЛЯ РЕДАКТИРОВАНИЯ

        /// <summary>
        /// Открытие окна редактирования департмента
        /// </summary>
        private static void OpenEditDepartmentWindowMethod(Department<int> department)
        {
            EditDepartmentWindow editDepartmentWindow = new(department);
            SetCenterPositionAndOpen(editDepartmentWindow);
        }

        /// <summary>
        /// Открытие окна редактирования позиции
        /// </summary>
        private static void OpenEditPositionWindowMethod(Position<int> position)
        {
            EditPositionWindow editPositionWindow = new(position);
            SetCenterPositionAndOpen(editPositionWindow);
        }

        /// <summary>
        /// Открытие окна редактирования пользователя
        /// </summary>
        private static void OpenEditUserWindowMethod(User<int> user)
        {
            EditUserWindow editUserWindow = new(user);
            SetCenterPositionAndOpen(editUserWindow);
        }

        /// <summary>
        /// Открытие окна редактирования клиента
        /// </summary>
        private static void OpenEditClientWindowMethod(Client<int> client)
        {
            EditClientWindow editClientWindow = new(client);
            SetCenterPositionAndOpen(editClientWindow);
        }

        /// <summary>
        /// Открытие окна редактирования депозита
        /// </summary>
        private static void OpenEditDepositWindowMethod(Deposit<int> deposit)
        {
            EditDepositWindow editDepositWindow = new(deposit);
            SetCenterPositionAndOpen(editDepositWindow);
        }
        #endregion

        #region КОМАНДЫ ОТКРЫТИЯ ОКОН СОЗДАНИЯ

        /// <summary>
        /// Команда открытия окна для создания нового департмента
        /// </summary>
        private readonly RelayCommand openAddNewDepartmentWnd;
        public RelayCommand OpenAddNewDepartmentWnd
        {
            get
            {
                return openAddNewDepartmentWnd ?? new RelayCommand(obj =>
               {
                   OpenAddDepartmentWindowMethod();
               }
                    );
            }
        }

        /// <summary>
        /// Команда открытия окна для создания новой позиции
        /// </summary>
        private readonly RelayCommand openAddNewPositionWnd;
        public RelayCommand OpenAddNewPositionWnd
        {
            get
            {
                return openAddNewPositionWnd ?? new RelayCommand(obj =>
                {
                    OpenAddPositionWindowMethod();
                }
                    );
            }
        }

        /// <summary>
        /// Команда открытия окна для создания нового юзера
        /// </summary>
        private readonly RelayCommand openAddNewUserWnd;
        public RelayCommand OpenAddNewUserWnd
        {
            get
            {
                return openAddNewUserWnd ?? new RelayCommand(obj =>
                {
                    OpenAddUserWindowMethod();
                }
                    );
            }
        }

        /// <summary>
        /// Команда открытия окна для создания нового клиента
        /// </summary>
        private readonly RelayCommand openAddNewClientWnd;
        public RelayCommand OpenAddNewClientWnd
        {
            get
            {
                return openAddNewClientWnd ?? new RelayCommand(obj =>
                {
                    OpenAddClientWindowMethod();
                }
                    );
            }
        }

        /// <summary>
        /// Команда открытия окна для создания нового депозита
        /// </summary>
        private readonly RelayCommand openAddNewDepositWnd;
        public RelayCommand OpenAddNewDepositWnd
        {
            get
            {
                return openAddNewDepositWnd ?? new RelayCommand(obj =>
                {
                    OpenAddDepositWindowMethod();
                }
                    );
            }
        }

        private readonly RelayCommand openTransferDepositWnd;
        public RelayCommand OpenTransferDepositWnd
        {
            get
            {
                return openTransferDepositWnd ?? new RelayCommand(obj =>
                {
                    OpenTransferDepositWindowMethod();
                }
                    );
            }
        }

        #endregion

        #region КОМАНДЫ ДОБАВЛЕНИЯ


        /// <summary>
        /// Добавление 1к клиентов
        /// </summary>
        private readonly RelayCommand createClients;

        public RelayCommand CreateClients
        {
            get
            {
                return createClients ?? new RelayCommand(obj =>
                {

                    //CreateTestClients();

                    //
                    Thread thread = new(new ThreadStart(CreateTestClients));
                    thread.Start();

                    //Dispatcher dispatcher = new Dispatcher.(new ThreadStart(delegate { new DataManageVM().CreateTestClients(); }));

                    //засовываем в таск общий метод, включающий обновление данных
                    //var a = new Action(CreateTestClients);
                    //var task = new Task(a);
                    //task.Start();

                    //CreateTestClients().Start();
                });
            }
        }

        

        /// <summary>
        /// Добавление департамента
        /// </summary>
        private RelayCommand addNewDepartment;
        public RelayCommand AddNewDepartment
        {
            get
            {
                return addNewDepartment ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateDepartment(DepartmentName);
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }

        }

        /// <summary>
        /// Добавление позиции
        /// </summary>
        private RelayCommand addNewPosition;
        public RelayCommand AddNewPosition
        {
            get
            {
                return addNewPosition ?? new RelayCommand(obj =>
                {
                    try
                    {
                        Window wnd = obj as Window;
                        string resultStr = "";
                        if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
                        {
                            SetRedBlockControl(wnd, "NameBlock");
                        }
                        if (PositionSalary <= 0)
                        {
                            throw new MyException("Неправильно заполнено поле 'Зарплата'", 1);
                            //SetRedBlockControl(wnd, "SalaryBlock");
                        }
                        if (PositionMaxNumber <= 0)
                        {
                            throw new MyException("Неправильно заполнено поле 'Max number'", 2);
                            //SetRedBlockControl(wnd, "MaxNumberBlock");
                        }
                        if (PositionDepartment == null)
                        {
                            MessageBox.Show("Укажите отдел");
                        }
                        else
                        {
                            resultStr = DataWorker.CreatePosition(PositionName, PositionSalary, PositionMaxNumber, PositionDepartment);
                            UpdateAllDataView();
                            ShowMessageToUser(resultStr);
                            SetNullValuesToProperties();
                            wnd.Close();
                        }
                    }
                    catch (MyException ex) when (ex.ErrorCode==1)
                    {
                        ShowMessageToUser(ex.Message);
                    }
                    catch (MyException ex) when (ex.ErrorCode == 2)
                    {
                        ShowMessageToUser(ex.Message);
                    }
                }
                );
            }
        }

        /// <summary>
        /// Добавление юзера
        /// </summary>
        private RelayCommand addNewUser;
        public RelayCommand AddNewUser
        {
            get
            {
                return addNewUser ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (UserName == null || UserName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");
                    }
                    if (UserSurName == null || UserSurName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "SurNameBlock");
                    }
                    if (UserPhone == null)
                    {
                        SetRedBlockControl(wnd, "PhoneBlock");
                    }
                    if (UserDateOfBirth == DateTime.MinValue)
                    {
                        SetRedBlockControl(wnd, "DateOfBirthDP");
                    }
                    if (UserPosition == null)
                    {
                        MessageBox.Show("Укажите позицию");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateUser(UserName, UserSurName, UserPhone, UserPosition, UserDateOfBirth);
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        /// <summary>
        /// Добавление клиента
        /// </summary>
        private RelayCommand addNewClient;
        public RelayCommand AddNewClient
        {
            get
            {
                return addNewClient ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (ClientName == null || ClientName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");
                    }
                    if (ClientSurName == null || ClientSurName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "SurNameBlock");
                    }
                    if (ClientPhone == null)
                    {
                        SetRedBlockControl(wnd, "PhoneBlock");
                    }
                    if (ClientDateOfBirth == DateTime.MinValue)
                    {
                        SetRedBlockControl(wnd, "DateOfBirthDP");
                    }
                    else
                    {
                        RadioButton rdbtn = wnd.FindName("YesVIP") as RadioButton;
                        if (rdbtn.IsChecked == true)
                        {
                            ClientIsVIP = true;
                        }
                        resultStr = DataWorker.CreateClient(ClientName, ClientSurName, ClientPhone, ClientIsVIP, ClientDateOfBirth);
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        /// <summary>
        /// Добавление депозита
        /// </summary>
        private readonly RelayCommand addNewDeposit;
        public RelayCommand AddNewDeposit
        {
            get
            {
                return addNewDeposit ?? new RelayCommand(obj =>
                {
                    //Подписываемся на событие
                    DataWorker.Notify += ShowMessageToUser;
                    DataWorker.Notify += WriteToLog;
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (DepositClient == null)
                    {
                        MessageBox.Show("Укажите клиента");
                    }
                    if (DepositPercent == 0)
                    {
                        SetRedBlockControl(wnd, "PercentBlock");
                    }
                    if (DepositStartSum == 0)
                    {
                        SetRedBlockControl(wnd, "StartSumBlock");
                    }
                    RadioButton rdtn = wnd.FindName("YesCapitalized") as RadioButton;
                    if (rdtn.IsChecked == true)
                    {
                        DepositIsCapitalized = true;
                    }
                    if (DepositResponsibleEmployee == null)
                    {
                        MessageBox.Show("Укажите ответственного");
                    }
                    if (DepositDateOfStart == DateTime.MinValue)
                    {
                        SetRedBlockControl(wnd, "DateOfStartDP");
                    }
                    if (DepositMonthsCount == 0)
                    {
                        SetRedBlockControl(wnd, "MonthsCountBlock");
                    }
                    else
                    {
                        DataWorker.CreateDeposit(DepositClient, DepositPercent, DepositStartSum, DepositIsCapitalized,
                            DepositDateOfStart, DepositMonthsCount, DepositResponsibleEmployee);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }


        #endregion

        #region КОМАНДЫ ОТКРЫТИЯ ОКОН ДЛЯ РЕДАКТИРОВАНИЯ

        private RelayCommand openEditItemWnd;
        public RelayCommand OpenEditItemWnd
        {
            get
            {
                return openEditItemWnd ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если сотрудник
                    if (SelectedTabItem.Name == "UsersTab" && SelectedUser != null)
                    {
                        OpenEditUserWindowMethod(SelectedUser);
                    }
                    //если позиция
                    if (SelectedTabItem.Name == "PositionTab" && SelectedPosition != null)
                    {
                        OpenEditPositionWindowMethod(SelectedPosition);
                    }
                    //если отдел
                    if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                    {
                        OpenEditDepartmentWindowMethod(SelectedDepartment);
                    }
                    //если клиент
                    if (SelectedTabItem.Name == "ClientsTab" && SelectedClient != null)
                    {
                        OpenEditClientWindowMethod(SelectedClient);
                    }
                    //если депозит
                    if (SelectedTabItem.Name == "DepositsTab" && SelectedDeposit != null)
                    {
                        OpenEditDepositWindowMethod(SelectedDeposit);
                    }
                });
            }
        }
        #endregion

        #region КОМАНДЫ РЕДАКТИРОВАНИЯ

        /// <summary>
        /// Команда редактирования юзера
        /// </summary>
        private RelayCommand editUser;
        public RelayCommand EditUser
        {
            get
            {
                return editUser ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран сотрудник";
                    string noPositionStr = "Не выбрана новая должность";
                    if (SelectedUser != null && UserPosition != null)
                    {
                        if (UserPosition != null)
                        {
                            resultStr = DataWorker.EditUser(SelectedUser, UserName, UserSurName, UserPhone, UserPosition, UserDateOfBirth);
                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noPositionStr);
                    }
                    else ShowMessageToUser(resultStr);
                });
            }
        }

        /// <summary>
        /// Команда редактирования позиции
        /// </summary>
        private RelayCommand editPosition;
        public RelayCommand EditPosition
        {
            get
            {
                return editPosition ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана позиция";
                    string noDepartmentStr = "Не выбран новый отдел";
                    if (SelectedPosition != null)
                    {
                        if (PositionDepartment != null)
                        {
                            resultStr = DataWorker.EditPosition(SelectedPosition, PositionName, PositionMaxNumber,PositionSalary,PositionDepartment);
                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noDepartmentStr);
                    }
                    else ShowMessageToUser(resultStr);
                });
            }
        }

        /// <summary>
        /// Команда редактирования департмента
        /// </summary>
        private RelayCommand editDepartment;
        public RelayCommand EditDepartment
        {
            get
            {
                return editDepartment ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран отдел";
                    if (SelectedDepartment != null)
                    {
                        resultStr = DataWorker.EditDepartment(SelectedDepartment,DepartmentName);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                    else ShowMessageToUser(resultStr);
                });
            }
        }

        /// <summary>
        /// Команда редактирования клиента
        /// </summary>
        private RelayCommand editClient;
        public RelayCommand EditClient
        {
            get
            {
                return editClient ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран клиент";
                    if (SelectedClient != null)
                    {
                        RadioButton rdbtn = window.FindName("YesVIP") as RadioButton;
                        if (rdbtn.IsChecked == true)
                        {
                            ClientIsVIP = true;
                        }
                        else ClientIsVIP = false;
                        resultStr = DataWorker.EditClient(SelectedClient, ClientName, ClientSurName, ClientPhone, ClientIsVIP, ClientDateOfBirth);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                    else ShowMessageToUser(resultStr);
                });
            }
        }

        /// <summary>
        /// Команда редактирования депозита
        /// </summary>
        private RelayCommand editDeposit;
        public RelayCommand EditDeposit
        {
            get
            {
                return editDeposit ?? new RelayCommand(obj =>
                {
                    //Подписываемся на событие
                    DataWorker.Notify += ShowMessageToUser;
                    DataWorker.Notify += WriteToLog;
                    Window window = obj as Window;
                    string resultStr = "Не выбран депозит";
                    string noClientStr = "Не выбран новый клиент или ответственный сотрудник";
                    if (SelectedDeposit != null)
                    {
                        if (DepositClient != null && DepositResponsibleEmployee !=null)
                        {
                            DataWorker.EditDeposit(SelectedDeposit,DepositClient,DepositPercent,
                                DepositStartSum,DepositIsCapitalized,DepositDateOfStart,DepositMonthsCount,DepositResponsibleEmployee);
                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            //ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noClientStr);
                    }
                    else ShowMessageToUser(resultStr);
                });
            }
        }
        #endregion

        #region КОМАНДЫ ПЕРЕВОДА ДЕПОЗИТОВ

        private RelayCommand transferDeposit;
        public RelayCommand TransferDeposit
        {
            get
            {
                return transferDeposit ?? new RelayCommand(obj =>
                {
                    //Подписываемся на событие
                    DataWorker.Notify += ShowMessageToUser;
                    DataWorker.Notify += WriteToLog;
                    Window window = obj as Window;
                    string resultStr = "Не выбран депозит";
                    string noSumToTransfer = "Не выбрана сумма перевода или счет перевода";
                    if (SelectedDeposit != null)
                    {
                        if (DestinationDeposit != null && DepositSumToTransfer > 0)
                        {
                            DataWorker.TransferDeposit(SelectedDeposit.Id, DestinationDeposit.Id, DepositSumToTransfer);
                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            //ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noSumToTransfer);
                    }
                    else ShowMessageToUser(resultStr);
                });
            }
        }
        #endregion

        #region КОМАНДЫ УДАЛЕНИЯ

        /// <summary>
        /// удалить тестовых клиентов
        /// </summary>
        private readonly RelayCommand deleteClients;

        public RelayCommand DeleteClients
        {
            get
            {
                return deleteClients ?? new RelayCommand(obj =>
                {
                    //DeleteTestClients().Start();
                    var a = new Action(DeleteTestClients);
                    var task = new Task(a);
                    task.Start();
                });
            }
        }

        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если сотрудник
                    if(SelectedTabItem.Name == "UsersTab" && SelectedUser!=null)
                    {
                        resultStr = DataWorker.DeleteUser(SelectedUser);
                        UpdateAllDataView();
                    }
                    //если позиция
                    if (SelectedTabItem.Name == "PositionTab" && SelectedPosition != null)
                    {
                        resultStr = DataWorker.DeletePosition(SelectedPosition);
                        UpdateAllDataView();
                    }
                    //если отдел
                    if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                    {
                        resultStr = DataWorker.DeleteDepartment(SelectedDepartment);
                        UpdateAllDataView();
                    }
                    //если клиент
                    if (SelectedTabItem.Name == "ClientsTab" && SelectedClient != null)
                    {
                        resultStr = DataWorker.DeleteClient(SelectedClient);
                        UpdateAllDataView();
                    }
                    //если депозит
                    if (SelectedTabItem.Name == "DepositsTab" && SelectedDeposit != null)
                    {
                        //Подписываемся на события
                        DataWorker.Notify += ShowMessageToUser;
                        DataWorker.Notify += WriteToLog;
                        DataWorker.DeleteDeposit(SelectedDeposit);
                        UpdateAllDataView();
                    }
                    //обновление
                    SetNullValuesToProperties();
                    //ShowMessageToUser(resultStr);
                }
                    );
            }
        }
        #endregion

        #region РАЗНЫЕ МЕТОДЫ
        private static void SetRedBlockControl(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new(message);
            SetCenterPositionAndOpen(messageView);
        }

        public void WriteToLog(string message)
        {
            string writePath = @"log.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                MessageView messageView = new(e.Message);
            }
        }

        /// <summary>
        /// Task для асинхронного добавления данных
        /// </summary>
        public void CreateTestClients()
        {
            DataWorker.CreateTestClients();
            UpdateAllDataView();
            SetNullValuesToProperties();
            ShowMessageToUser("Добавлено");
        }

        public void DeleteTestClients()
        {
            DataWorker.DeleteTestClients();
            UpdateAllDataView();
            SetNullValuesToProperties();
            ShowMessageToUser("Удаление завершено");
        }

        #endregion

        #region UPDATE VIEWS

        /// <summary>
        /// Обнуляет свойства
        /// </summary>
        private void SetNullValuesToProperties()
        {
            //отписываемя от событий
            DataWorker.Notify -= ShowMessageToUser;
            DataWorker.Notify -= WriteToLog;
            //для пользователя
            UserName = null;
            UserSurName = null;
            UserPhone = null;
            UserPosition = null;
            UserDateOfBirth = DateTime.MinValue;
            //для позиции
            PositionName = null;
            PositionSalary = 0;
            PositionMaxNumber = 0;
            PositionDepartment = null;
            //для отдела
            DepartmentName = null;
            //для клиента
            ClientName = null;
            ClientSurName = null;
            UserPhone = null;
            ClientIsVIP = false;
            UserDateOfBirth = DateTime.MinValue;
            //для депозита
            DepositClient = null;
            DepositPercent = 0;
            DepositStartSum = 0;
            DepositIsCapitalized = false;
            DepositDateOfStart = DateTime.MinValue;
            DepositMonthsCount = 0;
            DepositResponsibleEmployee = null;
            DepositSumToTransfer = 0;
            DestinationDeposit = null;
        }
        private async void UpdateAllDataView()
        {
            UpdateAllDepartmentsView();
            UpdateAllPositionsView();
            UpdateAllUsersView();
            UpdateAllClientsView();
            UpdateAllDepositsView();
        }

        private void UpdateAllDepartmentsView()
        {
            AllDepartments = DataWorker.GetAllDepartments();
            //Dispatcher.Invoke(() => MainWindow.AllDepartmentsView.ItemsSource = null);
            MainWindow.AllDepartmentsView.ItemsSource = null;
            MainWindow.AllDepartmentsView.Items.Clear();
            MainWindow.AllDepartmentsView.ItemsSource = AllDepartments;
            MainWindow.AllDepartmentsView.Items.Refresh();
        }

        private void UpdateAllPositionsView()
        {
            AllPositions = DataWorker.GetAllPositions();
            MainWindow.AllPositionsView.ItemsSource = null;
            MainWindow.AllPositionsView.Items.Clear();
            MainWindow.AllPositionsView.ItemsSource = AllPositions;
            MainWindow.AllPositionsView.Items.Refresh();
        }

        private void UpdateAllUsersView()
        {
            AllUsers = DataWorker.GetAllUsers();
            MainWindow.AllUsersView.ItemsSource = null;
            MainWindow.AllUsersView.Items.Clear();
            MainWindow.AllUsersView.ItemsSource = AllUsers;
            MainWindow.AllUsersView.Items.Refresh();
        }

        private void UpdateAllClientsView()
        {
            AllClients = DataWorker.GetAllClients();
            MainWindow.AllClientsView.ItemsSource = null;
            MainWindow.AllClientsView.Items.Clear();
            MainWindow.AllClientsView.ItemsSource = AllClients;
            MainWindow.AllClientsView.Items.Refresh();
        }

        private void UpdateAllDepositsView()
        {
            AllDeposits = DataWorker.GetAllDeposits();
            MainWindow.AllDepositsView.ItemsSource = null;
            MainWindow.AllDepositsView.Items.Clear();
            MainWindow.AllDepositsView.ItemsSource = AllDeposits;
            MainWindow.AllDepositsView.Items.Refresh();
        }
        #endregion

    }
}
