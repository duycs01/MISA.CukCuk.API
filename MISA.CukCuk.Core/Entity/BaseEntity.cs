using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entity
{
    public class BaseEntity
    {
        #region Property

        /// <summary>
        ///  Ngày tạo bản ghi
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        
        /// <summary>
        /// Người tạo bản ghi
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa bản ghi
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa bản ghi
        /// </summary>
        public string ModifiedBy { get; set; }
        #endregion
    }
}
