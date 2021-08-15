using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repository
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Lấy tất cả nhân viên
        /// </summary>
        /// <returns>Trả ra danh sách nhân viên</returns>
        /// Created by: duylv-14/8/2021
        List<Employee> GetAll();

        /// <summary>
        /// Lọc nhân viên theo theo tiêu chí
        /// </summary>
        /// <param name="filterName">Mã hách hàng, họ tên, số điện thoại </param>
        /// <returns>Trả ra danh sách nhân viên lọc theo tiêu chí</returns>
        /// Created by: duylv-14/8/2021
        List<Employee> Filter(string filterName);

        /// <summary>
        /// Lấy thông tin nhân viên theo id
        /// </summary>
        /// <param name="employeeId">Id của nhân viên</param>
        /// <returns>Trả ra thông tin nhân viên</returns>
        /// Created by: duylv-14/8/2021
        Employee GetById(Guid? employeeId);

        /// <summary>
        /// Thêm thông tin nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int Insert(Employee employee);

        /// <summary>
        /// Sửa thông tin nhân viên
        /// </summary>
        /// <param name="employeeId">Id nhân viên</param>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int Update(Guid employeeId, Employee employee);

        /// <summary>
        /// Xóa thông tin nhân viên
        /// </summary>
        /// <param name="employeeId">Id nhân viên</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteById(Guid employeeId);

        /// <summary>
        /// Xóa danh sách thông tin nhân viên
        /// </summary>
        /// <param name="listId">danh sách id nhân viên</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteListId(List<Guid> listId);
    }
}
