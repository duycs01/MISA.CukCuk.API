using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeServices
    {
        #region DECLEAR
        IEmployeeRepository _employeeRepository;
        #endregion

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="employeerRepository">Kết nối database</param>
        public EmployeeService(IEmployeeRepository employeerRepository):base(employeerRepository)
        {
            _employeeRepository = employeerRepository;
        }

        /// <summary>
        /// Lấy mã nhân viên mới nhất
        /// </summary>
        /// <returns>Trả ra mã nhân viên mới nhất</returns>
        /// Created by: duylv - 21/08/2021
        public string NewCode()
        {
            var employeeCode = _employeeRepository.NewCode();

            var  strVal = string.Empty;
            var numbVal = string.Empty;

            int numb = 0;

            for (int i = 0; i < employeeCode.Length; i++)
            {
                if (Char.IsDigit(employeeCode[i]))
                {
                    numbVal += employeeCode[i];
                }
                else
                {
                    strVal += employeeCode[i];
                }
            }

            if (numbVal.Length > 0)
                numb = Int32.Parse(numbVal) + 1;

            var res = string.Empty;

            var checkDuplicate = _employeeRepository.CheckDuplicate(null, res);
            do
            {
                res = $"{strVal}{numb + 1}";
            } while (checkDuplicate);
            return res;
        }

        /// <summary>
        /// Validate bản ghi riêng
        /// </summary>
        /// <param name="employee">Thông tin bản ghi</param>
        /// <returns>true là không bị lỗi - false bị lỗi </returns>
        /// Created by: duylv - 18/08/2021
        protected override bool ValidateCustom(Employee employee)
        {
            // Kiểm tra trùng mã
            var duplicate = _employeeRepository.CheckDuplicate(employee.EmployeeId,employee.EmployeeCode);
            if (_serviceResult.IsValid == true && duplicate == true)
            {
                var data = new
                {
                    PropertyName = Resources.Resources.Name_EmployeeCode,
                    ErrorInfo = Resources.Resources.Duplicate_EmployeeCode,
                };
                _serviceResult.Data = data;
                _serviceResult.IsValid = false;
            }

            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool testEmail = Regex.IsMatch(employee.Email, regex, RegexOptions.IgnoreCase);
            if (_serviceResult.IsValid == true && testEmail == false)
            {
                _serviceResult.Messenger = Resources.Resources.Error_Email;
                _serviceResult.IsValid = false;
            }
            var regexNum = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
            bool testPhoneNumber = Regex.IsMatch(employee.PhoneNumber, regexNum);
            if(_serviceResult.IsValid == true && testPhoneNumber == false)   
            {
                _serviceResult.Messenger = Resources.Resources.Error_PhoneNumber;
                _serviceResult.IsValid = false;
            }
            return _serviceResult.IsValid;
        }
    }
}
