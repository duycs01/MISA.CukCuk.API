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
        /// Lấy danh sách dữ liệu
        /// </summary>
        /// <returns>ServiceResult - Kết quả sử lí qua nghiệp vụ</returns>
        /// Created by: duylv-14/8/2021
        ServiceResult GetAll ();

        /// <summary>
        /// Lấy thông tin dữ liệu qua entityId
        /// </summary>
        /// <param name="entityId">entityId dữ liệu</param>
        /// <returns>ServiceResult - Kết quả sử lí qua nghiệp vụ</returns>
        /// Created by: duylv-14/8/2021
        ServiceResult GetById (Guid entityId);

        /// <summary>
        /// Thêm thông tin dữ liệu
        /// </summary>
        /// <param name="entity">Thông tin dữ liệu</param>
        /// <returns>ServiceResult - Kết quả sử lí qua nghiệp vụ</returns>
        /// Created by: duylv-14/8/2021
        ServiceResult Insert (MISAEntity entity);

        /// <summary>
        /// Sửa thông tin dữ liệu
        /// </summary>
        /// <param name="entityId">entityId dữ liệu</param>
        /// <param name="entity">Thông tin dữ liệu</param>
        /// <returns>ServiceResult - Kết quả sử lí qua nghiệp vụ</returns>
        /// Created by: duylv-14/8/2021
        ServiceResult Update (Guid id, MISAEntity entity);

        /// <summary>
        /// Xóa thông tin dữ liệu
        /// </summary>
        /// <param name="entityId">entityId dữ liệu</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        ServiceResult DeleteById (Guid entityId);

    }
}
