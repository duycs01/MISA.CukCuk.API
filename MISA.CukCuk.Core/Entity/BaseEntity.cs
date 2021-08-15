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
        ///  Ngày tạo dữ liệu
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        
        /// <summary>
        /// Người tạo dữ liệu
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa dữ liệu
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa dữ liệu
        /// </summary>
        public string ModifiedBy { get; set; }
        #endregion
    }
}
