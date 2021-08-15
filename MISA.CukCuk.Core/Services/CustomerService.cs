using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class CustomerService : ICustomerServices
    {
        ICustomerRepository _customerRepository;
        ServiceResult _serviceResult;
        public CustomerService()
        {
            _serviceResult = new ServiceResult();
        }
        public ServiceResult Insert(Customer customer)
        {
            //Sử lí nghiệp vụ

            //Kết nối database
             _serviceResult.Data = _customerRepository.Insert(customer);
            return _serviceResult;
        }

        public ServiceResult Update(Guid customerId, Customer customer)
        {
            //Sử lí nghiệp vụ

            //Kết nối database
            _serviceResult.Data = _customerRepository.Update(customerId,customer);
            return _serviceResult;
        }
    }
}
