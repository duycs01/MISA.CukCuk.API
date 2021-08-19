using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CulCuk.Infractructure.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infractructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public Paging GetEmployeePaging(string filterName, Guid? positionId, Guid? departmentId, int pageSize, int pageIndex)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            var input = filterName == null ? string.Empty : filterName;
            dynamicParameters.Add("@EmployeeFilter", input);
            dynamicParameters.Add("@PositionId", positionId);
            dynamicParameters.Add("@DepartmentId", departmentId);

            dynamicParameters.Add("@PageIndex", pageIndex);
            dynamicParameters.Add("@PageSize", pageSize);
            dynamicParameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dynamicParameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);


            var resPaging = _dbConnection.Query("Proc_GetEmployeeFilterPaging", dynamicParameters, commandType: CommandType.StoredProcedure);

            
            Paging _paging = new Paging();
            _paging.Data = resPaging.ToList();
            _paging.TotalPage = dynamicParameters.Get<Int32>("@TotalPage");
            _paging.TotalRecord = dynamicParameters.Get<Int32>("@TotalRecord");

            return _paging;
        }
        public bool CheckDuplicate(Guid employeeId, string employeeCode)
        {

            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@employeeCode", employeeCode);

            var sqlCommand = "SELECT * FROM Employee WHERE EmployeeCode = @employeeCode";

            var res = _dbConnection.QueryFirstOrDefault(sqlCommand, dynamicParameters);
            
            if (res != null && res.EmployeeId != employeeId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
