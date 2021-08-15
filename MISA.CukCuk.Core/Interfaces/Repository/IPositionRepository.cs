using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repository
{
    public interface IPositionRepository
    {
        /// <summary>
        /// Lấy tất cả vị trí
        /// </summary>
        /// <returns>Trả ra danh sách vị trí</returns>
        /// Created by: duylv-14/8/2021
        List<Position> GetAll();

        /// <summary>
        /// Lọc vị trí theo theo tiêu chí
        /// </summary>
        /// <param name="filterName">Mã hách hàng, họ tên, số điện thoại </param>
        /// <returns>Trả ra danh sách vị trí lọc theo tiêu chí</returns>
        /// Created by: duylv-14/8/2021
        List<Position> Filter(string filterName);

        /// <summary>
        /// Lấy thông tin vị trí theo id
        /// </summary>
        /// <param name="positionId">Id của vị trí</param>
        /// <returns>Trả ra thông tin vị trí</returns>
        /// Created by: duylv-14/8/2021
        Position GetById(Guid? positionId);

        /// <summary>
        /// Thêm thông tin vị trí
        /// </summary>
        /// <param name="position">Thông tin vị trí</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int Insert(Position position);

        /// <summary>
        /// Sửa thông tin vị trí
        /// </summary>
        /// <param name="positionId">Id vị trí</param>
        /// <param name="position">Thông tin vị trí</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int Update(Guid positionId, Position position);

        /// <summary>
        /// Xóa thông tin vị trí
        /// </summary>
        /// <param name="positionId">Id vị trí</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteById(Guid positionId);

        /// <summary>
        /// Xóa danh sách thông tin vị trí
        /// </summary>
        /// <param name="listId">danh sách id vị trí</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteListId(List<Guid> listId);
    }
}
