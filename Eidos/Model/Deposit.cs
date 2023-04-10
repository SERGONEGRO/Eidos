using System;
using System.ComponentModel.DataAnnotations.Schema;
using MyLib;

namespace TheBank2.Model
{
    public class Deposit<T>
    {
        /// <summary>
        /// id
        /// </summary>
        public T Id { get; set; }

        public int ClientId { get; set; }
        /// <summary>
        /// Вкладчик
        /// </summary>
        public virtual Client<int> Client { get; set; }

        /// <summary>
        /// Процент вклада
        /// </summary>
        public double DepositPercent { get; set; }

        /// <summary>
        /// Начальная сумма вклада
        /// </summary>
        public int StartSum { get; set; }

        /// <summary>
        /// Есть ли капитализация процентов?
        /// </summary>
        public bool IsCapitalized { get; set; }

        /// <summary>
        /// Текущая сумма
        /// </summary>
        [NotMapped]
        public double CurrentSum
        {
            get
            {
                int monthsLeft = DateOfEnd < DateTime.Now ? MonthsCount : MonthCalculate();
                return IsCapitalized ? CalculationWithCapitalizing(monthsLeft) : CalculationWithoutCapitalizing(monthsLeft);
            }
            set { }
        }

        /// <summary>
        /// Подсчёт месяцев
        /// </summary>
        /// <returns></returns>
        public int MonthCalculate()
        {
            if (DateTime.Now > DateOfStart)
            {
                TimeSpan timeSpan = DateTime.Now - DateOfStart;
                return timeSpan.Days / 30;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// сумма по окончанию вклада
        /// </summary>
        [NotMapped]
        public double EndSum => IsCapitalized ? CalculationWithCapitalizing(MonthsCount) : CalculationWithoutCapitalizing(MonthsCount);

        /// <summary>
        /// Вычисление суммы депозита по количеству месяцев без капитализации
        /// </summary>
        /// <param name="months"></param>
        /// <returns></returns>
        private double CalculationWithoutCapitalizing(int months)
        {
            //если VIP - +1%
            if (DepositClient.IsVIP) { DepositPercent++; }
            double result = StartSum;
            for (int i = 0; i < months; i++)
            {
                result += StartSum * DepositPercent / 100 / 12;
            }
            return result;
        }

        /// <summary>
        /// Вычисление суммы депозита по количеству месяцев с капитализацией
        /// </summary>
        /// <param name="months"></param>
        /// <returns></returns>
        private double CalculationWithCapitalizing(int months)
        {
            //если VIP - +1%
            if (DepositClient.IsVIP) { DepositPercent++; }
            double result = StartSum;
            for (int i = 0; i < months; i++)
            {
                result += result * DepositPercent / 100 / 12;
            }
            return result;
        }

        /// <summary>
        /// Перерасчёт депозита ( после перевода денег)
        /// </summary>
        /// <param name="sumToTransfer"></param>
        public void DepositRecalculation(int sumToTransfer)
        {
            StartSum = (int)CurrentSum + sumToTransfer;
            MonthsCount -= MonthCalculate();
            DateOfStart = DateTime.Now;
        }

        /// <summary>
        /// Дата начала вклада
        /// </summary>
        public DateTime DateOfStart { get; set; }


        /// <summary>
        /// количество месяцев вклада
        /// </summary>
        public int MonthsCount { get; set; }

        /// <summary>
        /// Дата окончания вклада
        /// </summary>
        public DateTime DateOfEnd => DateOfStart.AddMonths(MonthsCount);

        public int ResponsibleEmployeeId { get; set; }
        /// <summary>
        /// Ответственный сотрудник
        /// </summary>
        public virtual User<int> ResponsibleEmployee { get; set; }

        /// <summary>
        /// свойство ищущее ответственного сотрудника, привязанного к данному депозиту
        /// </summary>
        [NotMapped]
        public User<int> DepositUser => DataWorker.GetUserByResponsibleEmployeeId(ResponsibleEmployeeId);
        /// <summary>
        /// свойство ищущее клиента данного депозита
        /// </summary>
        [NotMapped]
        public Client<int> DepositClient => DataWorker.GetClientByClientId(ClientId);
    }
}

