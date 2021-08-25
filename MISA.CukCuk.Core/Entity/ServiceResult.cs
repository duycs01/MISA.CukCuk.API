using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entity
{
    public class ServiceResult
    {
        #region Property
        /// <summary>
        /// Kiểm tra lỗi
        /// </summary>
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Thông báo
        /// </summary>
        public string Messenger { get; set; }
        #endregion
    }
}
