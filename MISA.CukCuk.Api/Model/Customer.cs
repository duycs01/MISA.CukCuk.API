using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Model
{
    public class Customer
    {
        #region Property
        /// <summary>
        /// ID khách hàng
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Họ khách hàng
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Họ và tên khách hàng
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        public int MyProperty { get; set; }
        #endregion
    }
}
