using MISA.CukCuk.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MISA.CukCuk.Core.MISAAttribute.MISAAttribute;

namespace MISA.CukCuk.Core.Entity
{
    public class Customer : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>

        [MISARequired("Mã khách hàng")]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Họ và đệm
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string LastName { get; set; }

        [MISARequired("Họ và tên")]
        /// <summary>
        /// Tên đầy đủ
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
        public DateTime? DateOfBirth { get; set; }

        [MISARequired("Email")]
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        [MISARequired("Số điện thoại")]
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Số tiền ghi nợ
        /// </summary>
        public double DebitAmount { get; set; }

        /// <summary>
        /// Mã thành viên
        /// </summary>
        public string MemberCardCode { get; set; }

        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Mã số thuế công ty
        /// </summary>
        public string CompanyTaxCode { get; set; }

        /// <summary>
        /// Đang dừng hoạt động
        /// </summary>
        public int IsStopFollow { get; set; }

        /// <summary>
        /// id nhóm khách hàng
        /// </summary>~
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        [MISANotMap]
        public string CustomerGroupName { get; set; }

        /// <summary>
        /// Chú ý
        /// </summary>
        [MISANotMap]
        public string Attention { get; set; }

        /// <summary>
        /// Lỗi của file inport
        /// </summary>
        [MISANotMap]
        public List<ImportError> ImportError { get; set; } = new List<ImportError>();
        #endregion
    }

    /// <summary>
    /// Lỗi của file inport
    /// </summary>
    public class ImportError {
        /// <summary>
        /// Lỗi của file inport
        /// </summary>
        /// <param name="success">Kiểm tra lỗi hay hợp lệ</param>
        /// <param name="errorMessage">Thông báo lỗi</param>
        public ImportError(bool success, string errorMessage)
        {
            this.Success = success;
            this.ErrorMessage = errorMessage;
        }
        public bool Success { get; set; }
        public string  ErrorMessage { get; set; }
    }    
}
