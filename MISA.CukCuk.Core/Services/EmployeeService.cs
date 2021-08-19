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
        IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeerRepository):base(employeerRepository)
        {
            _employeeRepository = employeerRepository;
        }

        public Paging GetEmployeePaging(int limit, int offset)
        {
            throw new NotImplementedException();
        }
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
            var regexNum = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            bool testPhoneNumber = Regex.IsMatch(employee.PhoneNumber, regexNum);
            if(_serviceResult.IsValid == true && testPhoneNumber == false)   
            {
                _serviceResult.Messenger = "Số điện thoại sai rồi";
                _serviceResult.IsValid = false;
            }
            return _serviceResult.IsValid;
        }
    }
}
