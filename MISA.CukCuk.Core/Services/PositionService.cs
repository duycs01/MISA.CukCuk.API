using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class PositionService : IPositionServices
    {
        IPositionRepository _positionRepository ;
        ServiceResult _serviceResult;
        public PositionService()
        {
            _serviceResult = new ServiceResult();
        }
        public ServiceResult Insert(Position position)
        {
            //Sử lí nghiệp vụ

            //Kết nối database
            _serviceResult.Data = _positionRepository.Insert(position);
            return _serviceResult;

        }

        public ServiceResult Update(Guid positionId, Position position)
        {
            //Sử lí nghiệp vụ

            //Kết nối database
            _serviceResult.Data = _positionRepository.Insert(position);
            return _serviceResult;
        }
    }
}
