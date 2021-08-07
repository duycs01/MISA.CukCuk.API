using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Model
{
    public class BaseEntity
    {
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
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        /// Người sửa dữ liệu
        /// </summary>
        public string ModifyBy { get; set; }
    }
}
