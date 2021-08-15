using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repository
{
    public interface ICustomerGroupRepository
    {

        /// <summary>
        /// Lấy tất cả nhóm khách hàng
        /// </summary>
        /// <returns>Trả ra danh sách nhóm khách hàng</returns>
        /// Created by: duylv-14/8/2021
        List<CustomerGroup> GetAll();

        /// <summary>
        /// Lọc nhóm khách hàng theo theo tiêu chí
        /// </summary>
        /// <param name="filterName">Mã hách hàng, họ tên, số điện thoại </param>
        /// <returns>Trả ra danh sách nhóm khách hàng lọc theo tiêu chí</returns>
        /// Created by: duylv-14/8/2021
        List<CustomerGroup> Filter(string filterName);

        /// <summary>
        /// Lấy thông tin nhóm khách hàng theo id
        /// </summary>
        /// <param name="customerGroupId">Id của nhóm khách hàng</param>
        /// <returns>Trả ra thông tin nhóm khách hàng</returns>
        /// Created by: duylv-14/8/2021
        CustomerGroup GetById(Guid? customerGroupId);

        /// <summary>
        /// Thêm thông tin nhóm khách hàng
        /// </summary>
        /// <param name="customerGroup">Thông tin nhóm khách hàng</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int Insert(CustomerGroup customerGroup);

        /// <summary>
        /// Sửa thông tin nhóm khách hàng
        /// </summary>
        /// <param name="customerGroupId">Id nhóm khách hàng</param>
        /// <param name="customerGroup">Thông tin nhóm khách hàng</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int Update(Guid customerGroupId, CustomerGroup customerGroup);

        /// <summary>
        /// Xóa thông tin nhóm khách hàng
        /// </summary>
        /// <param name="customerGroupId">Id nhóm khách hàng</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteById(Guid customerGroupId);

        /// <summary>
        /// Xóa danh sách thông tin nhóm khách hàng
        /// </summary>
        /// <param name="listId">danh sách id nhóm khách hàng</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteListId(List<Guid> listId);
    }

}
