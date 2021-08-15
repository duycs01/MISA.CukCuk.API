using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repository
{
    public interface IDepartmentRepository
    {
        /// <summary>
        /// Lấy tất cả phòng ban
        /// </summary>
        /// <returns>Trả ra danh sách phòng ban</returns>
        /// Created by: duylv-14/8/2021
        List<Department> GetAll();

        /// <summary>
        /// Lọc phòng ban theo theo tiêu chí
        /// </summary>
        /// <param name="filterName">Mã hách hàng, họ tên, số điện thoại </param>
        /// <returns>Trả ra danh sách phòng ban lọc theo tiêu chí</returns>
        /// Created by: duylv-14/8/2021
        List<Department> Filter(string filterName);

        /// <summary>
        /// Lấy thông tin phòng ban theo id
        /// </summary>
        /// <param name="departmentId">Id của phòng ban</param>
        /// <returns>Trả ra thông tin phòng ban</returns>
        /// Created by: duylv-14/8/2021
        Department GetById(Guid? departmentId);

        /// <summary>
        /// Thêm thông tin phòng ban
        /// </summary>
        /// <param name="department">Thông tin phòng ban</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int Insert(Department department);

        /// <summary>
        /// Sửa thông tin phòng ban
        /// </summary>
        /// <param name="departmentId">Id phòng ban</param>
        /// <param name="department">Thông tin phòng ban</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int Update(Guid departmentId, Department department);

        /// <summary>
        /// Xóa thông tin phòng ban
        /// </summary>
        /// <param name="departmentId">Id phòng ban</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteById(Guid departmentId);

        /// <summary>
        /// Xóa danh sách thông tin phòng ban
        /// </summary>
        /// <param name="listId">danh sách id phòng ban</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteListId(List<Guid> listId);
    }
}
