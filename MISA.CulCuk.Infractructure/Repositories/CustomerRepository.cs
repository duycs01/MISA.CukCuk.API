using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CukCuk.Core.MISAAttribute;
using MISA.CulCuk.Infractructure.Repositories;
using MySqlConnector;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        public bool CheckDuplicate(Guid? customerId, string customerCode)
        {

            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@customerCode", customerCode);
            dynamicParameters.Add("@PhoneNumber", customerCode);
            dynamicParameters.Add("@Email", customerCode);


            var sqlCommand = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @customerCode " +
                "OR PhoneNumber = @PhoneNumber OR Email = @Email";

            var res = _dbConnection.QueryFirstOrDefault(sqlCommand, dynamicParameters);

            if (res != null && res.CustomerId != customerId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int DeleteListId(dynamic listId)
        {
            var transaction = _dbConnection.BeginTransaction();
            DynamicParameters parameters = new DynamicParameters();
            var rowEffects = 0;
            foreach (var item in listId["CustomerId"])
            {
                parameters.Add("@CustomerId", Convert.ToString(item));
                rowEffects += _dbConnection.Execute($"Proc_DeleteCustomer",  parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
            }
            transaction.Commit();
            return rowEffects;
        }

        public Paging GetCustomerPaging(string filterName , Guid? customerGroupId, int pageSize, int pageIndex)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            var input = filterName == null ? string.Empty : filterName;
            dynamicParameters.Add("@CustomerFilter", input);
            dynamicParameters.Add("@CustomerGroupId", customerGroupId);

            dynamicParameters.Add("@PageIndex", pageIndex);
            dynamicParameters.Add("@PageSize", pageSize);
            dynamicParameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dynamicParameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);


            var resPaging = _dbConnection.Query("Proc_GetCustomerFilterPaging", dynamicParameters, commandType: CommandType.StoredProcedure);


            Paging _paging = new Paging();
            _paging.Data = resPaging.ToList();
            _paging.TotalPage = dynamicParameters.Get<Int32>("@TotalPage");
            _paging.TotalRecord = dynamicParameters.Get<Int32>("@TotalRecord");

            return _paging;
        }


        public int InsertListCustomer(List<Customer> listCustomers)
        {
            var transaction = _dbConnection.BeginTransaction();

            var customer = new Customer();
            var properties = customer.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name == $"CustomerId")
                {
                    prop.SetValue(customer, Guid.NewGuid());
                }
            }
            var parameters = MappingDBType(customer);
            var rowEffects = 0;
            for (int i = 0; i < 10; i++)
            {
                rowEffects += _dbConnection.Execute($"Proc_InsertCustomer", parameters, commandType: CommandType.StoredProcedure);
            }
            transaction.Commit();
            return rowEffects;
        }
    }

    #endregion 

 }

