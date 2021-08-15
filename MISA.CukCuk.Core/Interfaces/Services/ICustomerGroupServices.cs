using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{

    public interface ICustomerGroupServices
    {
        /// <summary>
        /// Thêm thông tin nhóm khách hàng
        /// </summary>
        /// <param name="customerGroup">Thông tin nhóm khách hàng</param>
        /// <returns>ServiceResult - Kết quả sử lí qua nghiệp vụ</returns>
        /// Created by: duylv-14/8/2021
        ServiceResult Insert(CustomerGroup customerGroup);

        /// <summary>
        /// Sửa thông tin nhóm khách hàng
        /// </summary>
        /// <param name="customerGroupId">Id nhóm khách hàng</param>
        /// <param name="customerGroup">Thông tin nhóm khách hàng</param>
        /// <returns>ServiceResult - Kết quả sử lí qua nghiệp vụ</returns>
        /// Created by: duylv-14/8/2021
        ServiceResult Update(Guid customerGroupId, CustomerGroup customerGroup);

    }
}
