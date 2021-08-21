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
    public class PositionService : BaseService<Position>,IPositionServices
    {
        IPositionRepository _positionRepository ;
        public PositionService(IPositionRepository positionRepository):base(positionRepository)
        {
            _positionRepository = positionRepository;
        }
       
    }
}
