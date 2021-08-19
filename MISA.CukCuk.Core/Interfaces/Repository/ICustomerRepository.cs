using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repository
{
    public interface ICustomerRepository: IBaseRepository<Customer>
    {
 

        /// <summary>
        /// Lọc khách hàng theo theo tiêu chí
        /// </summary>
        /// <param name="filterName">Mã hách hàng, họ tên, số điện thoại </param>
        /// <returns>Trả ra danh sách khách hàng lọc theo tiêu chí</returns>
        /// Created by: duylv-14/8/2021
        List<Customer> Filter(string filterName);

        
        /// <summary>
        /// Xóa danh sách thông tin khách hàng
        /// </summary>
        /// <param name="listId">danh sách id khách hàng</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteListId(List<Guid> listId);

        /// <summary>
        /// Kiểm tra trùng mã khách hàng
        /// </summary>
        /// <param name="customerCode">Mã khách hàng</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        bool CheckDuplicate(string customerCode);

        Paging GetCustomerPaging(string filterName, Guid? customerGroupId, int pageSize, int pageIndex);

    }

}
