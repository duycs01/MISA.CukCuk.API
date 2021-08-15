using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface IDepartmentServices
    {


        /// <summary>
        /// Thêm thông tin phòng ban
        /// </summary>
        /// <param name="department">Thông tin phòng ban</param>
        /// <returns>ServiceResult - Kết quả sử lí qua nghiệp vụ</returns>
        /// Created by: duylv-14/8/2021
        ServiceResult Insert(Department department);

        /// <summary>
        /// Sửa thông tin phòng ban
        /// </summary>
        /// <param name="departmentId">Id phòng ban</param>
        /// <param name="department">Thông tin phòng ban</param>
        /// <returns>ServiceResult - Kết quả sử lí qua nghiệp vụ</returns>
        /// Created by: duylv-14/8/2021
        ServiceResult Update(Guid departmentId, Department department);

        
    }

}
