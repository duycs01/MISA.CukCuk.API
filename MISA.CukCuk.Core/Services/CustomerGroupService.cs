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
    public class CustomerGroupService : ICustomerGroupServices
    {
        ICustomerGroupRepository _customerGroupRepository;
        ServiceResult _serviceResult;
        public CustomerGroupService()
        {
            _serviceResult = new ServiceResult();
        }
        public ServiceResult Insert(CustomerGroup customerGroup)
        {
            //Sử lí nghiệp vụ

            //Kết nối database
            _serviceResult.Data = _customerGroupRepository.Insert(customerGroup);
            return _serviceResult;
        }

        public ServiceResult Update(Guid customerGroupId, CustomerGroup customerGroup)
        {
            //Sử lí nghiệp vụ

            //Kết nối database
            _serviceResult.Data = _customerGroupRepository.Update(customerGroupId, customerGroup);
            return _serviceResult;
        }
    }
}
