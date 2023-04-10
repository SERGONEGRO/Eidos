using System;

namespace MyLib
{
    public class Person
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string SurName { get; set; }
        /// <summary>
        /// Фамилия+Имя
        /// </summary>
        public string FullName => Name + " " + SurName;
        /// <summary>
        /// телефон
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Возраст
        /// </summary>
        public byte Age
        {
            get
            {
                if (DateTime.Now <= DateOfBirth)
                {
                    return 0;
                }

                int n = DateTime.Now.Year - DateOfBirth.Year;
                if (DateOfBirth.DayOfYear > DateTime.Now.DayOfYear)
                {
                    --n;
                }

                return n > 99 ? (byte)99 : (byte)n;
            }
        }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}
