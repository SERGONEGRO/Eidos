using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBank2.Model
{
    public class Position<T>
    {
        /// <summary>
        /// id
        /// </summary>
        public T Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int MaxNumber { get; set; }
        public List<User<int>> Users { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department<int> Department { get; set; }

        /// <summary>
        /// свойство ищущее департмент, к которому привязана позиция
        /// </summary>
        [NotMapped]
        public Department<int> PositionDepartment => DataWorker.GetDepartmentById(DepartmentId);

        /// <summary>
        /// свойство ищущее юзеров, привязанных к позиции
        /// </summary>
        [NotMapped]
        public List<User<int>> PositionUsers
        {
            get
            {
                return DataWorker.GetAllUserstByPositionId(Id);
            }
        }
    }
}
