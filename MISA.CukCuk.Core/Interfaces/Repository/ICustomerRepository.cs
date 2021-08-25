using Microsoft.AspNetCore.Http;
using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repository
{
    public interface ICustomerRepository: IBaseRepository<Customer>
    {
 
        
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
        bool CheckDuplicate(Guid? customerId, string customerCode);

        /// <summary>
        /// Phân trang và tìm kiếm theo họ tên, mã khách hàng, nhóm khách hàng
        /// </summary>
        /// <param name="filterName">tìm kiếm theo họ tên, mã khách hàng,</param>
        /// <param name="customerGroupId">id nhóm khách hàng</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageIndex">vị trí của trang</param>
        /// <returns></returns>
        Paging GetCustomerPaging(string filterName, Guid? customerGroupId, int pageSize, int pageIndex);


        /// <summary>
        /// Thêm danh sách khách hàng vào hệ thống
        /// </summary>
        /// <param name="listCustomers">Danh sách khách hàng</param>
        /// <returns>Số bản ghi thêm được</returns>
        /// Created by: duylv - 21/08/2021
        /// 
        int InsertListCustomer(List<Customer> listCustomers);

        /// <summary>
        /// Lấy mã khách hàng mới nhất
        /// </summary>
        /// <returns>Trả ra mã khách hàng mới nhất</returns>
        /// Created by: duylv - 21/08/2021
        public string NewCode();
    }

}
