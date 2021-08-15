using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface ICustomerServices
    {

        /// <summary>
        /// Thêm thông tin khách hàng
        /// </summary>
        /// <param name="customer">Thông tin khách hàng</param>
        /// <returns>ServiceResult - Kết quả sử lí qua nghiệp vụ</returns>
        /// Created by: duylv-14/8/2021
        ServiceResult Insert(Customer customer);

        /// <summary>
        /// Sửa thông tin khách hàng
        /// </summary>
        /// <param name="customerId">Id khách hàng</param>
        /// <param name="customer">Thông tin khách hàng</param>
        /// <returns>ServiceResult - Kết quả sử lí qua nghiệp vụ</returns>
        /// Created by: duylv-14/8/2021
        ServiceResult Update(Guid customerId,Customer customer);

       
    }
}
