using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infractructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public int DeleteById(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public int DeleteListId(List<Guid> listId)
        {
            throw new NotImplementedException();
        }

        public List<Employee> Filter(string filterName)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee GetById(Guid? employeeId)
        {
            throw new NotImplementedException();
        }

        public int Insert(Employee employee)
        {
            throw new NotImplementedException();
        }

        public int Update(Guid employeeId, Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
