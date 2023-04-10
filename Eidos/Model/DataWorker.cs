using System;
using System.Collections.Generic;
using System.Linq;
using Eidos.Data;

namespace Eidos.Model
{
    internal class DataWorker
    {
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
        /// Получение юзеров по id позиции
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<User<int>> GetAllUserstByPositionId<T>(T id)
        {
            using ApplicationContext db = new();
            List<User<int>> users = (from user in GetAllUsers() where user.PositionId == Convert.ToInt32(id) select user).ToList();
            return users;
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

        #endregion

        #region МЕТОДЫ СОЗДАНИЯ

        /// <summary>
        /// Создать Департамент
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CreateDepartment(string name, string address)
        {
            string result = "Уже существует";
            using ApplicationContext db = new();
            //проверяем, суще ли отдел
            bool checkIsExist = db.Departments.Any(el => el.Name == name);
            if (!checkIsExist)
            {
                Department<int> newDepartment = new() { Name = name, Address = address};
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

        #endregion

        #region МЕТОДЫ РЕДАКТИРОВАНИЯ
        /// <summary>
        /// Редактировать отдел
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static string EditDepartment(Department<int> oldDepartment, string newName, string newAddress)
        {
            string result = "Такого отдела не существует";
            using (ApplicationContext db = new())
            {
                Department<int> department = db.Departments.FirstOrDefault(d => d.Id == oldDepartment.Id);
                department.Name = newName;
                department.Address = newAddress;
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

        #endregion
    }
}
