using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface IEmployeeServices
    {

        /// <summary>
        /// Thêm thông tin nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        ServiceResult Insert(Employee employee);

        /// <summary>
        /// Sửa thông tin nhân viên
        /// </summary>
        /// <param name="employeeId">Id nhân viên</param>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        ServiceResult Update(Guid employeeId, Employee employee);

    }
}
