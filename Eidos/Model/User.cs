using System.ComponentModel.DataAnnotations.Schema;
using MyLib;

namespace TheBank2.Model
{
    public class User<T> : Person
    {
        #region Свойства
        /// <summary>
        /// id
        /// </summary>
        public T Id { get; set; }

        public T PositionId { get; set; }
        public virtual Position<int> Position { get; set; }

        /// <summary>
        /// Ставка
        /// </summary>
        public int Rate { get; set; }

        /// <summary>
        /// Количество отработанных часов
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// Получение позиции для данного сотрудника
        /// </summary>
        [NotMapped]
        public Position<int> UserPosition => DataWorker.GetPositionById(PositionId);

        #endregion
    }
}
