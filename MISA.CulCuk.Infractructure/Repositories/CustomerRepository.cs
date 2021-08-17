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
    public class CustomerRepository : BaseRepository<Customer>
    {
        #region DECLEAR
        string _connectionString = string.Empty;
        IDbConnection _dbConnection = null;

        #endregion

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="configuration"></param>
        public CustomerRepository(IConfiguration configuration) :base(configuration)
        {
            _connectionString = configuration.GetConnectionString("MISACukCukConnectionString");
            _dbConnection = new MySqlConnection(_connectionString); 
        }
        #region Method
   
        /// <summary>
        /// Kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Truyền vào mã nhân viên</param>
        /// <returns>true/false</returns>
        /// CreateBy duylv - 12/08/2021
        public bool CheckDuplicate(string employeeCode)
        {

            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@employeeCode", employeeCode);

            var sqlCommand = "SELECT EmployeeCode FROM Employee WHERE EmployeeCode = @employeeCode";

            var res = _dbConnection.QueryFirstOrDefault(sqlCommand, dynamicParameters);
            if (res == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    #endregion 

 }

