using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerServices
    {
        ICustomerRepository _customerRepository;
        ServiceResult _serviceResult;
        public CustomerService(IBaseRepository<Customer> baseRepository) : base(baseRepository)
        {
            _serviceResult = new ServiceResult();
        }

        public bool CheckDuplicate(string customerCode)
        {
           return _customerRepository.CheckDuplicate(customerCode)
        }

        public int DeleteListId(List<Guid> listId)
        {
            throw new NotImplementedException();
        }

        protected override bool ValidateCustom(Customer customer)
        {
            return false;
            //// Mã nhân viên bắt buộc phải có
            //if (customer.CustomerCode == "" || customer.CustomerCode == null)
            //{
            //    var errObj = new
            //    {
            //        devMsg = Resources.Resources.EmtyNull_CustomerCode,
            //        userMsg = Resources.Resources.EmtyNull_CustomerCode,
            //        //errorCode = "misa-001",
            //        //moreInfor = "...",
            //        //traceId = "1232",
            //    };
            //     _serviceResult.Messenger = errObj;
            //    return false;
            //}

            //// Kiểm tra trùng mã
            //var duplicate =  checkDuplicate(customer.CustomerCode);
            //if (employeeCode == true)
            //{
            //    var errObj = new
            //    {
            //        devMsg = MISA.CukCuk.Core.Resources.Resources.Duplicate_EmployeeCode,
            //        userMsg = MISA.CukCuk.Core.Resources.Resources.Duplicate_EmployeeCode,
            //        //errorCode = "misa-001",
            //        //moreInfor = "...",
            //        //traceId = "1232",
            //    };
            //    return StatusCode(400, errObj);
            //}

            //var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            //bool isValid = Regex.IsMatch(employees.Email, regex, RegexOptions.IgnoreCase);
            //if (isValid)
            //{
            //    var errObj = new
            //    {
            //        devMsg = MISA.CukCuk.Core.Resources.Resources.Error_Email,
            //        userMsg = MISA.CukCuk.Core.Resources.Resources.Error_Email,
            //        //errorCode = "misa-001",
            //        //moreInfor = "...",
            //        //traceId = "1232",
            //    };
            //    return StatusCode(400, errObj);
            //}
            //return base.ValidateCustom();
        }
    }
}
