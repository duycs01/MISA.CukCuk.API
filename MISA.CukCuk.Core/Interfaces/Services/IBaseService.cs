using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface IBaseService <MISAEntity>
    {
        /// <summary>
        /// Lấy danh sách bản ghi
        /// </summary>
        /// <returns>ServiceResult - Kết quả sử lí qua nghiệp vụ</returns>
        /// Created by: duylv-14/8/2021
        ServiceResult GetAll ();

        /// <summary>
        /// Lấy thông tin bản ghi qua Id
        /// </summary>
        /// <param name="entityId">Id bản ghi</param>
        /// <returns>ServiceResult - Kết quả sử lí qua nghiệp vụ</returns>
        /// Created by: duylv-14/8/2021
        ServiceResult GetById (Guid entityId);

        /// <summary>
        /// Thêm thông tin bản ghi
        /// </summary>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns>ServiceResult - Kết quả sử lí qua nghiệp vụ</returns>
        /// Created by: duylv-14/8/2021 
        ServiceResult Insert(MISAEntity entity);

        /// <summary>
        /// Sửa thông tin bản ghi
        /// </summary>
        /// <param name="entityId">Id bản ghi</param>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns>ServiceResult - Kết quả sử lí qua nghiệp vụ</returns>
        /// Created by: duylv-14/8/2021
        ServiceResult Update (Guid entityId, MISAEntity entity);

        /// <summary>
        /// Xóa thông tin bản ghi
        /// </summary>
        /// <param name="entityId">Id bản ghi</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        ServiceResult DeleteById (Guid entityId);

    }
}
