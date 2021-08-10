using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Model
{
    public class Employee:BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ và đệm
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string LastName { get; set; }
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
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Lương tháng
        /// </summary>
        public string Salary { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân / căn cước
        /// </summary>
        public int IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp Số chưng minh / căn cước
        /// </summary>
        public DateTime IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp Số chưng minh / căn cước
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Ngày gia nhập
        /// </summary>
        public DateTime JoinDate { get; set; }

        /// <summary>
        /// Trạng thái 
        /// </summary>
        public int MartialStatus { get; set; }

        /// <summary>
        /// Nền tảng giáo dục
        /// </summary>
        public int EducationalBackground { get; set; }

        /// <summary>
        /// Trình độ chuyên môn
        /// </summary>
        public Guid QualificationId { get; set; }

        /// <summary>
        /// Khóa ngoại của chức vụ
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Khóa ngoại của vị trí
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// Trạng thái công việc
        /// </summary>
        public int WorkStatus { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string PersonalTaxCode { get; set; }
        #endregion
    }
}
