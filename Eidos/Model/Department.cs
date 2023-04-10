using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBank2.Model
{
    public class Department<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
        public List<Position<int>> Positions { get; set; }
        //public List<string> Cabinets { get; set; }

        /// <summary>
        /// свойство ищущее список позиций, привязанных к департаменту
        /// </summary>
        [NotMapped]
        public List<Position<int>> DepartmentPositions => DataWorker.GetAllPositionsByDepartmentId(Id);
    }
}
