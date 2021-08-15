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
    public class DepartmentService : IDepartmentServices
    {
        IDepartmentRepository _departmentRepository;
        ServiceResult _serviceResult;
        public DepartmentService()
        {
            _serviceResult = new ServiceResult();
        }
        public ServiceResult Insert(Department department)
        {
            //Sử lí nghiệp vụ

            //Kết nối database
            _serviceResult.Data = _departmentRepository.Insert(department);
            return _serviceResult;
        }

        public ServiceResult Update(Guid departmentId, Department department)
        {
            //Sử lí nghiệp vụ

            //Kết nối database
            _serviceResult.Data = _departmentRepository.Update(departmentId, department);
            return _serviceResult;
        }
    }
}
