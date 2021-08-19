using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Repository
{
    public interface IEmployeeRepository :IBaseRepository<Employee>
    {
        Paging GetEmployeePaging(string filterName, Guid? positionId, Guid? departmentId, int pageSize, int pageIndex);
        bool CheckDuplicate(Guid employeeId, string employeeCode);

    }
}
