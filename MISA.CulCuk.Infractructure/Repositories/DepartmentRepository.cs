using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infractructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public int DeleteById(Guid departmentId)
        {
            throw new NotImplementedException();
        }

        public int DeleteListId(List<Guid> listId)
        {
            throw new NotImplementedException();
        }

        public List<Department> Filter(string filterName)
        {
            throw new NotImplementedException();
        }

        public List<Department> GetAll()
        {
            throw new NotImplementedException();
        }

        public Department GetById(Guid? departmentId)
        {
            throw new NotImplementedException();
        }

        public int Insert(Department department)
        {
            throw new NotImplementedException();
        }

        public int Update(Guid departmentId, Department department)
        {
            throw new NotImplementedException();
        }
    }
}
