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
        /// Kiểm tra trùng mã khách hàng, số điện thoại, emaiil 
        /// </summary>
        /// <param name="input">Truyền vào mã khách hàng, số điện thoại, emaiil</param>
        /// <returns>true là đã trùng/false không trùng</returns>
        /// CreateBy duylv - 12/08/2021
        public bool CheckDuplicate(Guid? customerId, string input)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@customerCode", input);
            dynamicParameters.Add("@PhoneNumber", input);
            dynamicParameters.Add("@Email", input);


            var sqlCommand = "SELECT * FROM Customer WHERE CustomerCode = @customerCode " +
                "OR PhoneNumber = @PhoneNumber OR Email = @Email LIMIT 1";

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

        /// <summary>
        /// Xóa danh sách khách hàng
        /// </summary>
        /// <param name="listId">Danh sách khách hàng</param>
        /// <returns>Số hang xóa được</returns>
        /// CreateBy duylv - 16/08/2021
        /// 
        public int DeleteListId(List<Guid> listId)
        {
            var transaction = _dbConnection.BeginTransaction();
            DynamicParameters parameters = new DynamicParameters();
            var rowEffects = 0;
            foreach (var item in listId)
            {
                parameters.Add("@CustomerId", item.ToString());
                rowEffects += _dbConnection.Execute($"Proc_DeleteCustomer",  parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
            }
            transaction.Commit();
            return rowEffects;
        }

        /// <summary>
        /// Thực hiện phân trang và lọc bản ghi
        /// </summary>
        /// <param name="filterName">Lọc bản ghi theo tên, số điện thoại, mã khách hàng</param>
        /// <param name="customerGroupId">Id nhóm khách hàng</param>
        /// <param name="pageSize">Số lượng bản ghi trên một trang</param>
        /// <param name="pageIndex">Vị trí của trang</param>
        /// <returns>Tổng số trang, tổng số bản ghi,  vad data</returns>
        /// CreateBy duylv - 19/08/2021
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


        /// <summary>
        /// Thêm danh sách khách hàng vào data
        /// </summary>
        /// <param name="listCustomers">Danh sách khách hàng</param>
        /// <returns>Số lượng thêm được</returns>
        /// CreateBy duylv - 20/08/2021
        public int InsertListCustomer(List<Customer> listCustomers)
        {
            var rowEffects = 0;
            var transaction = _dbConnection.BeginTransaction();
            foreach (var customer in listCustomers)
            {
                customer.CustomerId = Guid.NewGuid();
                var parameters = MappingDBType(customer);
                rowEffects += _dbConnection.Execute($"Proc_InsertCustomer", param:parameters, transaction:transaction, commandType: CommandType.StoredProcedure);
            }
            transaction.Commit();
            return rowEffects;
        }

        /// <summary>
        /// Lấy mã có ngày thêm vào mới nhất
        /// </summary>
        /// <returns>Trả về mã có ngày mới nhất</returns>
        /// CreateBy duylv - 21/08/2021
        public string NewCode()
        {
            var sqlCommand = "SELECT CustomerCode FROM Customer ORDER BY CreatedDate DESC LIMIT 1";
            var res = _dbConnection.QueryFirstOrDefault<string>(sqlCommand);
            return res;
        }
    }
    #endregion 

 }

