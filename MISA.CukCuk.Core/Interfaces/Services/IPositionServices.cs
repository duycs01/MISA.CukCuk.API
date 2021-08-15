using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface IPositionServices
    {
     

        /// <summary>
        /// Thêm thông tin vị trí
        /// </summary>
        /// <param name="position">Thông tin vị trí</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        ServiceResult Insert(Position position);

        /// <summary>
        /// Sửa thông tin vị trí
        /// </summary>
        /// <param name="positionId">Id vị trí</param>
        /// <param name="position">Thông tin vị trí</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        ServiceResult Update(Guid positionId, Position position);

    }

}
