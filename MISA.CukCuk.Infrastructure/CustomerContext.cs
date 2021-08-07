using MISA.CukCuk.Api.Model;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infrastructure
{
    public class CustomerContext
    {
        #region
        // Lấy danh sách khách hàng
        public List<Customer> GetCustomers()
        {
            // Kết nối tới database
            var connectionString = "Host - 47.241.69.179" +
                "DataBase = MISA.CukCuk_Demo+NVMANH;" +
                "User Id = dev" +
                "Passwork = 12345678";


            // khởi tạo commanText
            IDbConnection dbConnection = new MySqlConnector

            //Trả về dữ liệu

        }

        // Lấy thông tin khách hàng theo id

        // Thêm mới khách hàng

        // Sửa khách hàng

        // Xóa khách hàng

        #endregion
    }
}
