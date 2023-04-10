using System;
using System.Collections.Generic;
using System.Linq;
using TheBank2.Data;
using MyLib;

namespace TheBank2.Model
{
    internal class DataWorker
    {
        public delegate void DepositHandler(string message);
        public static event DepositHandler Notify;

        #region МЕТОДЫ, ВОЗВРАЩАЮЩИЕ ЗНАЧЕНИЯ

        /// <summary>
        /// Получить все департаменты
        /// </summary>
        /// <returns></returns>
        public static List<Department<int>> GetAllDepartments()
        {
            using ApplicationContext db = new();
            List<Department<int>> result = db.Departments.ToList();
            return result;
        }

        /// <summary>
        /// Получить все позиции
        /// </summary>
        /// <returns></returns>
        public static List<Position<int>> GetAllPositions()
        {
            using ApplicationContext db = new();
            List<Position<int>> result = db.Positions.ToList();
            return result;
        }

        /// <summary>
        /// Получить всех сотрудников
        /// </summary>
        /// <returns></returns>
        public static List<User<int>> GetAllUsers()
        {
            using ApplicationContext db = new();
            List<User<int>> result = db.Users.ToList();
            return result;
        }

        /// <summary>
        /// Получить всех клиентов
        /// </summary>
        /// <returns></returns>
        public static List<Client<int>> GetAllClients()
        {
            using ApplicationContext db = new();
            List<Client<int>> result = db.Clients.ToList();
            return result;
        }

        /// <summary>
        /// Получить все депозиты
        /// </summary>
        /// <returns></returns>
        public static List<Deposit<int>> GetAllDeposits()
        {
            using ApplicationContext db = new();
            List<Deposit<int>> result = db.Deposits.ToList();
            return result;
        }

        /// <summary>
        /// Получение позиции по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Position<int> GetPositionById<T>(T id)
        {
            using ApplicationContext db = new();
            Position<int> pos = db.Positions.FirstOrDefault(p => p.Id == Convert.ToInt32(id));
            return pos;
        }

        /// <summary>
        /// Получение департмента по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Department<int> GetDepartmentById(int id)
        {
            using ApplicationContext db = new();
            Department<int> dep = db.Departments.FirstOrDefault(d => d.Id == id);
            return dep;
        }
        /// <summary>
        /// Получение пользователей по id позиции
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<User<int>> GetAllUserstByPositionId<T>(T id)
        {
            using ApplicationContext db = new();
            List<User<int>> users = (from user in GetAllUsers() where user.PositionId == Convert.ToInt32(id) select user).ToList();
            return users;
        }

        public static List<Client<int>> GetAllClientsByName(string name)
        {
            using ApplicationContext db = new();
            List<Client<int>> clients = (from client in GetAllClients() where client.Name.StartsWith("test") select client).ToList();
            //from client in GetAllClients() where client.Name.StartsWith("test") delete client;
            return clients;
        }

        /// <summary>
        /// Получение позиций по id департамента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Position<int>> GetAllPositionsByDepartmentId<T>(T id)
        {
            using ApplicationContext db = new();
            List<Position<int>> positions = (from position in GetAllPositions() where position.DepartmentId == Convert.ToInt32(id) select position).ToList();
            return positions;

        }
        /// <summary>
        /// Поулчение ответственного сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static User<int> GetUserByResponsibleEmployeeId(int id)
        {
            using ApplicationContext db = new();
            User<int> user = db.Users.FirstOrDefault(d => d.Id == id);
            return user;
        }

        /// <summary>
        /// Получение клиента по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Client<int> GetClientByClientId(int id)
        {
            using ApplicationContext db = new();
            Client<int> client = db.Clients.FirstOrDefault(d => d.Id == id);
            return client;
        }

        #endregion

        #region МЕТОДЫ СОЗДАНИЯ

        /// <summary>
        /// Создать Департамент
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CreateDepartment(string name)
        {
            string result = "Уже существует";
            using ApplicationContext db = new();
            //проверяем, суще ли отдел
            bool checkIsExist = db.Departments.Any(el => el.Name == name);
            if (!checkIsExist)
            {
                Department<int> newDepartment = new() { Name = name };
                db.Departments.Add(newDepartment);
                db.SaveChanges();
                result = "Сделано!";
            }
            return result;
        }

        /// <summary>
        /// Создать позицию
        /// </summary>
        /// <param name="name"></param>
        /// <param name="salary"></param>м
        /// <param name="maxnumber"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public static string CreatePosition(string name, decimal salary, int maxnumber, Department<int> department)
        {
            string result = "Уже существует";
            using ApplicationContext db = new();
            //проверяем, суще ли позиция
            bool checkIsExist = db.Positions.Any(el => el.Name == name && el.Salary == salary);
            if (!checkIsExist)
            {
                Position<int> newPosition = new()
                {
                    Name = name,
                    Salary = salary,
                    MaxNumber = maxnumber,
                    DepartmentId = department.Id
                };
                db.Positions.Add(newPosition);
                db.SaveChanges();
                result = "Сделано!";
            }
            return result;
        }

        /// <summary>
        /// создать сотрудника
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surName"></param>
        /// <param name="phone"></param>
        /// <param name="position"></param>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public static string CreateUser(string name, string surName, string phone, Position<int> position, DateTime dateOfBirth)
        {
            string result = "Уже существует";
            using ApplicationContext db = new();
            //проверяем, суще ли сотрудник
            bool checkIsExist = db.Users.Any(el => el.Name == name && el.SurName == surName && el.Position == position);
            if (!checkIsExist)
            {
                User<int> newUser = new()
                {
                    Name = name,
                    SurName = surName,
                    Phone = phone,
                    PositionId = position.Id,
                    DateOfBirth = dateOfBirth
                };
                db.Users.Add(newUser);
                db.SaveChanges();
                result = "Сделано!";
            }
            return result;
        }

        /// <summary>
        /// Создать Клиента
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surName"></param>
        /// <param name="phone"></param>
        /// <param name="isVip"></param>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public static string CreateClient(string name, string surName, string phone, bool isVip, DateTime dateOfBirth)
        {
            string result = "Уже существует";
            using ApplicationContext db = new();
            //проверяем, суще ли клиент
            bool checkIsExist = db.Clients.Any(el => el.Name == name && el.SurName == surName);
            if (!checkIsExist)
            {
                Client<int> newClient = new()
                {
                    Name = name,
                    SurName = surName,
                    Phone = phone,
                    IsVIP = isVip,
                    DateOfBirth = dateOfBirth
                };
                db.Clients.Add(newClient);
                db.SaveChanges();
                result = "Сделано!";
            }
            return result;
        }

        /// <summary>
        /// Создать тестовых клиентов
        /// </summary>
        public static async void CreateTestClients()
        {
            using ApplicationContext db = new();
            Random rand = new Random();
            for (int i = 0; i < 1000; i++)
            {
                Client<int> newClient = new()
                {
                    Name = "test" + rand.Next(1000000),
                    SurName = "test" + rand.Next(1000000),
                    Phone = rand.Next(1000000).ToString(),
                    IsVIP = true,
                    DateOfBirth = DateTime.Now
                };
                bool checkIsExist = db.Clients.Any(el => el.Name == newClient.Name && el.SurName == newClient.SurName);
                if (!checkIsExist)
                {
                    db.Clients.Add(newClient);
                }
                else
                {
                    i--;
                }
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Создать депозит
        /// </summary>
        /// <param name="client"></param>
        /// <param name="depositPercent"></param>
        /// <param name="startSum"></param>
        /// <param name="isCapitalized"></param>
        /// <param name="dateOfStart"></param>
        /// <param name="monthsCount"></param>
        /// <param name="responsibleEmployee"></param>
        /// <returns></returns>
        public static void CreateDeposit(Client<int> client, double depositPercent, int startSum, bool isCapitalized,
            DateTime dateOfStart, int monthsCount, User<int> responsibleEmployee)
        {
            using ApplicationContext db = new();
            //  НЕ проверяем, суще ли Депозит
            Deposit<int> newDeposit = new()
            {
                ClientId = client.Id,
                DepositPercent = depositPercent,
                StartSum = startSum,
                IsCapitalized = isCapitalized,
                DateOfStart = dateOfStart,
                MonthsCount = monthsCount,
                ResponsibleEmployeeId = responsibleEmployee.Id
            };
            db.Deposits.Add(newDeposit);
            db.SaveChanges();
            //вызываем событие
            Notify?.Invoke($"{newDeposit.DateOfStart} Создан депозит №{newDeposit.Id} на имя: {client.FullName} на сумму {newDeposit.StartSum}$.");
        }

        #endregion

        #region МЕТОДЫ УДАЛЕНИЯ
        /// <summary>
        /// удалить отдел
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static string DeleteDepartment(Department<int> department)
        {
            string result = "Такого отдела не существует";
            using (ApplicationContext db = new())
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                result = "Сделано! Отдел " + department.Name + "удален";
            }
            return result;
        }

        /// <summary>
        /// удалить позицию
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static string DeletePosition(Position<int> position)
        {
            string result = "Такой позиции не существует";
            using (ApplicationContext db = new())
            {
                db.Positions.Remove(position);
                db.SaveChanges();
                result = "Сделано! Отдел " + position.Name + "удален";
            }
            return result;
        }

        /// <summary>
        /// удалить сотрудника
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string DeleteUser(User<int> user)
        {
            string result = "Такой позиции не существует";
            using (ApplicationContext db = new())
            {
                db.Users.Remove(user);
                db.SaveChanges();
                result = "Сделано! Сотрудник " + user.Name + "удален";
            }
            return result;
        }

        /// <summary>
        /// удалить Клиента
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static string DeleteClient(Client<int> client)
        {
            string result = "Такой позиции не существует";
            using (ApplicationContext db = new())
            {
                db.Clients.Remove(client);
                db.SaveChanges();
                result = "Сделано! Клиент " + client.Name + "удален";
            }
            return result;
        }

        /// <summary>
        /// Удалить клиентов тестовых
        /// </summary>
        public static void DeleteTestClients()
        {
            using ApplicationContext db = new();
            foreach (Client<int> client in db.Clients)
            {
                if (client.Name.Length >=4)
                {
                    if (client.Name.Substring(0, 4) == "test")
                    {
                        db.Clients.Remove(client);
                    }
                }
            }
            //db.Clients.ToList().RemoveAll((x) => x.Name.Substring(0,4) == "test");
            db.SaveChanges();
        }

        /// <summary>
        /// удалить Депозит
        /// </summary>
        /// <param name="deposit"></param>
        /// <returns></returns>
        public static void DeleteDeposit(Deposit<int> deposit)
        {
            int id = deposit.Id;   //для лога
            using (ApplicationContext db = new())
            {
                db.Deposits.Remove(deposit);
                db.SaveChanges();
            }
            //вызываем событие
            Notify?.Invoke($"{DateTime.Now} Депозит №{id} удален from dataworker ");
        }
        #endregion

        #region МЕТОДЫ РЕДАКТИРОВАНИЯ
        /// <summary>
        /// Редактировать отдел
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static string EditDepartment(Department<int> oldDepartment, string newName)
        {
            string result = "Такого отдела не существует";
            using (ApplicationContext db = new())
            {
                Department<int> department = db.Departments.FirstOrDefault(d => d.Id == oldDepartment.Id);
                department.Name = newName;
                db.SaveChanges();
                result = "Сделано! Отдел " + department.Name + "изменен";
            }
            return result;
        }

        /// <summary>
        /// редактировать позицию
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static string EditPosition(Position<int> oldPosition, string newName, int newMaxNumber, decimal newSalary, Department<int> newDepartment)
        {
            string result = "Такой позиции не существует";
            ApplicationContext applicationContext = new();
            using (ApplicationContext db = applicationContext)
            {
                Position<int> position = db.Positions.FirstOrDefault(p => p.Id == oldPosition.Id);
                position.Name = newName;
                position.Salary = newSalary;
                position.MaxNumber = newMaxNumber;
                position.Department = newDepartment;
                db.SaveChanges();
                result = "Сделано! Позиция " + position.Name + "Изменена";
            }
            return result;
        }

        /// <summary>
        /// редактировать сотрудника
        /// </summary>
        /// <param name="oldUser"></param>
        /// <param name="newName"></param>
        /// <param name="newSurName"></param>
        /// <param name="newPhone"></param>
        /// <param name="newPosition"></param>
        /// <param name="newDateOfBirth"></param>
        /// <returns></returns>
        public static string EditUser(User<int> oldUser, string newName, string newSurName, string newPhone, Position<int> newPosition, DateTime newDateOfBirth)
        {
            string result = "Такого сотрудника не существует";
            using (ApplicationContext db = new())
            {
                User<int> user = db.Users.FirstOrDefault(p => p.Id == oldUser.Id);
                if (user != null)
                {
                    user.Name = newName;
                    user.SurName = newSurName;
                    user.Phone = newPhone;
                    user.Position = newPosition;
                    user.DateOfBirth = newDateOfBirth;
                    db.SaveChanges();
                    result = "Сделано! Сотрудник " + user.Name + "Изменен";
                }
            }
            return result;
        }

        /// <summary>
        /// Редактировать клиента
        /// </summary>
        /// <param name="oldClient"></param>
        /// <param name="newName"></param>
        /// <param name="newSurName"></param>
        /// <param name="newPhone"></param>
        /// <param name="newIsVip"></param>
        /// <param name="newDateOfBirth"></param>
        /// <returns></returns>
        public static string EditClient(Client<int> oldClient, string newName, string newSurName, string newPhone, bool newIsVip, DateTime newDateOfBirth)
        {
            string result = "Такого клиента не существует";
            using (ApplicationContext db = new())
            {
                Client<int> client = db.Clients.FirstOrDefault(p => p.Id == oldClient.Id);
                if (client != null)
                {
                    client.Name = newName;
                    client.SurName = newSurName;
                    client.Phone = newPhone;
                    client.IsVIP = newIsVip;
                    client.DateOfBirth = newDateOfBirth;
                    db.SaveChanges();
                    result = "Сделано! Клиент " + client.Name + "Изменен";
                }
            }
            return result;
        }

        /// <summary>
        /// Редактировать депозит
        /// </summary>
        /// <param name="oldDeposit"></param>
        /// <param name="newClient"></param>
        /// <param name="newDepositPercent"></param>
        /// <param name="newStartSum"></param>
        /// <param name="newIsCapitalized"></param>
        /// <param name="newDateOfStart"></param>
        /// <param name="newMonthsCount"></param>
        /// <param name="newResponsibleEmployee"></param>
        /// <returns></returns>
        public static void EditDeposit(Deposit<int> oldDeposit, Client<int> newClient, double newDepositPercent, int newStartSum, bool newIsCapitalized,
            DateTime newDateOfStart, int newMonthsCount, User<int> newResponsibleEmployee)
        {
            string result = "Такого Депозита не существует";
            using (ApplicationContext db = new())
            {
                Deposit<int> deposit = db.Deposits.FirstOrDefault(p => p.Id == oldDeposit.Id);
                int id = deposit.Id;            //для лога
                if (deposit != null)
                {
                    deposit.Client = newClient;
                    deposit.DepositPercent = newDepositPercent;
                    deposit.StartSum = newStartSum;
                    deposit.IsCapitalized = newIsCapitalized;
                    deposit.DateOfStart = newDateOfStart;
                    deposit.MonthsCount = newMonthsCount;
                    deposit.ResponsibleEmployee  = newResponsibleEmployee;
                    db.SaveChanges();
                    result = "Сделано! Депозит " + deposit.Id + "Изменен";
                }
                //вызываем событие
                Notify?.Invoke($"{DateTime.Now} Депозит №{id} отредактирован.");
            }
            
        }
        #endregion

        #region МЕТОД ПЕРЕВОДА ДЕПОЗИТА
        /// <summary>
        /// Переводит депозит
        /// </summary>
        /// <param name="sourceDepositId"></param>
        /// <param name="destinationDepositId"></param>
        /// <param name="depositSumToTransfer"></param>
        /// <returns></returns>
        internal static void TransferDeposit(int sourceDepositId, int destinationDepositId, int depositSumToTransfer)
        {
            using ApplicationContext db = new();
            Deposit<int> sourceDeposit = db.Deposits.FirstOrDefault(p => p.Id == sourceDepositId);
            Deposit<int> destinationDeposit = db.Deposits.FirstOrDefault(p => p.Id == destinationDepositId);
            int sourceId = sourceDeposit.Id;            //для лога
            int destinationID = destinationDeposit.Id;  //для лога
            if (DepositTransfer.CheckDepositForMoneyAmount(sourceDeposit.CurrentSum, depositSumToTransfer))
            {
                sourceDeposit.DepositRecalculation(-depositSumToTransfer);
                destinationDeposit.DepositRecalculation(depositSumToTransfer);
                db.SaveChanges();
                //вызываем событие
                Notify?.Invoke($"{DateTime.Now} Совершен перевод со счёта №{sourceId} на счёт № {destinationID} на сумму {depositSumToTransfer}$.");
            }
            else
            {
                //вызываем событие
                Notify?.Invoke($"{DateTime.Now} Перевод со счёта №{sourceId} на счёт № {destinationID} на сумму {depositSumToTransfer}$ не совершен!");
            }
        }

        #endregion 

    }
}
