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
        /// Lấy tất bản ghi
        /// </summary>
        /// <returns>Trả ra danh sách bản ghi</returns>
        /// Created by: duylv-15/8/2021
        List<MISAEntity> GetAll();

        /// <summary>
        /// Lấy thông tin bản ghi theo entityId
        /// </summary>
        /// <param name="entityId">Id của bản ghi</param>
        /// <returns>Trả ra thông tin bản ghi</returns>
        /// Created by: duylv-14/8/2021
        MISAEntity GetById(Guid entityId);

        /// <summary>
        /// Thêm thông tin bản ghi
        /// </summary>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int Insert(MISAEntity entity);

        /// <summary>
        /// Sửa thông tin bản ghi
        /// </summary>
        /// <param name="entityId">Id bản ghi</param>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int Update(Guid entityId, MISAEntity entity);

        /// <summary>
        /// Xóa thông tin bản ghi
        /// </summary>
        /// <param name="entityId">Id bản ghi</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteById(Guid entityId);
     
    }
}
