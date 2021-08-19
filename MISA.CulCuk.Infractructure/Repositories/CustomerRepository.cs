using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CukCuk.Core.MISAAttribute;
using MISA.CulCuk.Infractructure.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infractructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {

        #region Contructor
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="configuration"></param>
        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {
         
        }
        #endregion

        #region Method

        /// <summary>
        /// Kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="customerCode">Truyền vào mã nhân viên</param>
        /// <returns>true/false</returns>
        /// CreateBy duylv - 12/08/2021
        public bool CheckDuplicate(string customerCode)
        {

            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@customerCode", customerCode);

            var sqlCommand = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @customerCode";

            var res = _dbConnection.QueryFirstOrDefault(sqlCommand, dynamicParameters);

            if (res != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int DeleteListId(List<Guid> listId)
        {
            throw new NotImplementedException();
        }

        public List<Customer> Filter(string filterName)
        {
            throw new NotImplementedException();
        }

        public Paging GetCustomerPaging(string filterName , Guid? customerGroupId, int pageSize, int pageIndex)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            var input = filterName == null ? string.Empty : filterName;
            dynamicParameters.Add("@EmployeeFilter", input);
            dynamicParameters.Add("@CustomerGroupId", customerGroupId);

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
    }

    #endregion 

 }

