using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infractructure.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        public int DeleteById(Guid positionId)
        {
            throw new NotImplementedException();
        }

        public int DeleteListId(List<Guid> listId)
        {
            throw new NotImplementedException();
        }

        public List<Position> Filter(string filterName)
        {
            throw new NotImplementedException();
        }

        public List<Position> GetAll()
        {
            throw new NotImplementedException();
        }

        public Position GetById(Guid? positionId)
        {
            throw new NotImplementedException();
        }

        public int Insert(Position position)
        {
            throw new NotImplementedException();
        }

        public int Update(Guid positionId, Position position)
        {
            throw new NotImplementedException();
        }
    }
}
