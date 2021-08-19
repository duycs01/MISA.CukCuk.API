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

        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public int DeleteListId(List<Guid> listId)
        {
            throw new NotImplementedException();
        }

        public Paging GetCustomerPaging(string filterName, Guid? customerGroupId, int pageSize, int pageIndex)
        {
            return _customerRepository.GetCustomerPaging(filterName, customerGroupId, pageSize, pageIndex);
        }

        protected override bool ValidateCustom(Customer customer)
        {

            // Kiểm tra trùng mã
            var duplicate = _customerRepository.CheckDuplicate(customer.CustomerCode);
            if (_serviceResult.IsValid == true && duplicate == true)
            {
                var data = new
                {
                    PropertyName = Resources.Resources.Name_CustomerCode,
                    ErrorInfo = Resources.Resources.Duplicate_CustomerCode,
                };
                _serviceResult.Data = data;
                _serviceResult.Messenger = Resources.Resources.Duplicate_CustomerCode;
                _serviceResult.IsValid = false;
            }

            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool testEmail = Regex.IsMatch(customer.Email, regex, RegexOptions.IgnoreCase);
            if (_serviceResult.IsValid == true && testEmail)
            {
                var data = new
                {
                    PropertyName = Resources.Resources.Name_CustomerCode,
                    ErrorInfo = Resources.Resources.Error_Email,
                };
                _serviceResult.Data = data;
                _serviceResult.Messenger = Resources.Resources.Duplicate_CustomerCode;
                _serviceResult.IsValid = false;
            }
            return _serviceResult.IsValid;
        }
    }
}
