using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infractructure.Repositories
{
    public class CustomerGroupRepository : ICustomerGroupRepository
    {
        public int DeleteById(Guid customerGroupId)
        {
            throw new NotImplementedException();
        }

        public int DeleteListId(List<Guid> listId)
        {
            throw new NotImplementedException();
        }

        public List<CustomerGroup> Filter(string filterName)
        {
            throw new NotImplementedException();
        }

        public List<CustomerGroup> GetAll()
        {
            throw new NotImplementedException();
        }

        public CustomerGroup GetById(Guid? customerGroupId)
        {
            throw new NotImplementedException();
        }

        public int Insert(CustomerGroup customerGroup)
        {
            throw new NotImplementedException();
        }

        public int Update(Guid customerGroupId, CustomerGroup customerGroup)
        {
            throw new NotImplementedException();
        }
    }
}
