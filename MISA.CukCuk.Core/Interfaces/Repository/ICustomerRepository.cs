using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repository
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Lấy tất cả khách hàng
        /// </summary>
        /// <returns>Trả ra danh sách khách hàng</returns>
        /// Created by: duylv-14/8/2021
        List<Customer> GetAll();

        /// <summary>
        /// Lọc khách hàng theo theo tiêu chí
        /// </summary>
        /// <param name="filterName">Mã hách hàng, họ tên, số điện thoại </param>
        /// <returns>Trả ra danh sách khách hàng lọc theo tiêu chí</returns>
        /// Created by: duylv-14/8/2021
        List<Customer> Filter(string filterName);

        /// <summary>
        /// Lấy thông tin khách hàng theo id
        /// </summary>
        /// <param name="customerId">Id của khách hàng</param>
        /// <returns>Trả ra thông tin khách hàng</returns>
        /// Created by: duylv-14/8/2021
        Customer GetById(Guid? customerId);

        /// <summary>
        /// Thêm thông tin khách hàng
        /// </summary>
        /// <param name="customer">Thông tin khách hàng</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int Insert(Customer customer);

        /// <summary>
        /// Sửa thông tin khách hàng
        /// </summary>
        /// <param name="customerId">Id khách hàng</param>
        /// <param name="customer">Thông tin khách hàng</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int Update(Guid customerId, Customer customer);

        /// <summary>
        /// Xóa thông tin khách hàng
        /// </summary>
        /// <param name="customerId">Id khách hàng</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteById(Guid customerId);

        /// <summary>
        /// Xóa danh sách thông tin khách hàng
        /// </summary>
        /// <param name="listId">danh sách id khách hàng</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteListId(List<Guid> listId);
    }

}
