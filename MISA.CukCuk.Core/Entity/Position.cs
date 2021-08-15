using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entity
{
    public class Position:BaseEntity
    {
        #region Property

        public Guid PositionId { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public Guid ParentId { get; set; }
        #endregion
    }
}
