using Dapper;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infractructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public int DeleteById(Guid customerId)
        {
            // Truy cập vào database
            // 1. Khai báo thông tin database
            var connectionString = "Host = 47.241.69.179;" +
                "Database =  MF955_DuyLe_CukCuk;" +
                "User Id = dev;" +
                "Password = 12345678";

            // 2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            // Khai báo Dynamic Param
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@customerId", customerId);

            var sqlCommand = $"DELETE FROM Customer WHERE CustomerId=@customerId";
            var customer = dbConnection.Execute(sqlCommand, dynamicParameters);

            return customer;
        }

        public int DeleteListId(List<Guid> listId)
        {
            // Truy cập vào database
            // 1. Khai báo thông tin database
            var connectionString = "Host = 47.241.69.179;" +
                "Database =  MF955_DuyLe_CukCuk;" +
                "User Id = dev;" +
                "Password = 12345678";

            // 2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            // Khai báo Dynamic Param
            DynamicParameters dynamicParameters = new DynamicParameters();
            foreach (var item in listId)
            {
                dynamicParameters.Add("@customerId", item);
                var sqlCommand = $"DELETE FROM Customer WHERE CustomerId=@customerId";
                dbConnection.Execute(sqlCommand, dynamicParameters);
            }

            

            return customer;
        }

        public List<Customer> Filter(string filterName)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll()
        {
            try
            {
                var connectionString = "Host = 47.241.69.179;" +
                "Database =  MF955_DuyLe_CukCuk;" +
                "User Id = dev;" +
                "Password = 12345678";

                // 2. Khởi tạo đối tượng kết nối với database
                IDbConnection dbConnection = new MySqlConnection(connectionString);

                // 3. Lấy dữ liệu
                var sqlCommand = "SELECT * FROM Customer";
                var customers = dbConnection.Query<object>(sqlCommand);

                // Trả về cho client
                return (List<Customer>)customers;
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = CulCuk.Infractructure.Properties.Resources.Exception_ErrorMsg,

                    errorCode = "misa-001",
                    //moreInfor = "...",
                    //traceId = "",
                };
                return errObj;
            }

        }
    }

        public Customer GetById(Guid? customerId)
        {
            throw new NotImplementedException();
        }

        public int Insert(Customer customer)
        {
            throw new NotImplementedException();
        }

        public int Update(Guid customerId, Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
