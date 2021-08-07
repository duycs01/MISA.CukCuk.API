using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Api.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // GET, POST, PUT, DELETE
        [HttpGet]
        
        [HttpGet]
        public IActionResult GetCustomers(string customerId)
        {
            // Truy cập vào database
            // 1. Khai báo thông tin database
            var connectionString = "Host - 47.241.69.179" +
                "DataBase = MISA.CukCuk_Demo+NVMANH;" +
                "User Id = dev" +
                "Passwork = 12345678";

            // 2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3. Lấy dữ liệu
            var sqlCommand = "SELECT * FROM Customer WHERE CustomerId = @CustomerIdParam";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerIdParam", customerId);

            var customers = dbConnection.QueryFirstOrDefault<Customer>(sqlCommand);

            var response = StatusCode(200, customerId);
            return response;
        }

        [HttpPost]
        public string InsertCustomer([FromBody]List<Customer> customers,[FromHeader]string token)
        {
            return token;
        }
    }
}
