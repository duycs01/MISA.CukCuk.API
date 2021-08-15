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
    public class EmployeeService : IEmployeeServices
    {
        IEmployeeRepository _employeeRepository;
        ServiceResult _serviceResult;
        public EmployeeService()
        {
            _serviceResult = new ServiceResult();
        }
        public ServiceResult Insert(Employee employee)
        {
            //Sử lí nghiệp vụ

            //Kết nối database
            _serviceResult.Data = _employeeRepository.Insert(employee);
            return _serviceResult;
        }

        public ServiceResult Update(Guid employeeId, Employee employee)
        {
            //Sử lí nghiệp vụ

            //Kết nối database
            _serviceResult.Data = _employeeRepository.Update(employeeId,employee);
            return _serviceResult;
        }
    }
}
