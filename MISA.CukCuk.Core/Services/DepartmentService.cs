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
    public class DepartmentService : BaseService<Department>, IDepartmentServices
    {
        #region DECLEAR
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
        }
        #endregion
    }
}
