using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repository
{
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Lấy tất dữ liệu
        /// </summary>
        /// <returns>Trả ra danh sách dữ liệu</returns>
        /// Created by: duylv-15/8/2021
        List<MISAEntity> GetAll();

        /// <summary>
        /// Lấy thông tin dữ liệu theo entityId
        /// </summary>
        /// <param name="entityId">Id của dữ liệu</param>
        /// <returns>Trả ra thông tin dữ liệu</returns>
        /// Created by: duylv-14/8/2021
        MISAEntity GetById(Guid entityId);

        /// <summary>
        /// Thêm thông tin dữ liệu
        /// </summary>
        /// <param name="entity">Thông tin dữ liệu</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int Insert(MISAEntity entity);

        /// <summary>
        /// Sửa thông tin dữ liệu
        /// </summary>
        /// <param name="entityId">Id dữ liệu</param>
        /// <param name="entity">Thông tin dữ liệu</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int Update(Guid entityId, MISAEntity entity);

        /// <summary>
        /// Xóa thông tin dữ liệu
        /// </summary>
        /// <param name="entityId">Id dữ liệu</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteById(Guid entityId);
     
    }
}
