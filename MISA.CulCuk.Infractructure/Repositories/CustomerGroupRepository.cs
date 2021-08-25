using Microsoft.Extensions.Configuration;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CulCuk.Infractructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infractructure.Repositories
{
    public class CustomerGroupRepository : BaseRepository<CustomerGroup>, ICustomerGroupRepository
    {
        #region Contructor
        public CustomerGroupRepository(IConfiguration configuration):base(configuration)
        {

        }
        #endregion
    }
}
