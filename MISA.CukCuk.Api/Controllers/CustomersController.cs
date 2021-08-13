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
        /// <summary>
        /// Lấy toàn bộ dữ liệu Khách hàng
        /// </summary>
        /// <returns>Trả về danh sách khách hàng</returns>
        /// Created by - duylv - 11/08/2021
        [HttpGet]
        public IActionResult GetCustomers()
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
            var response = StatusCode(200, customers);
            return response;
        }


        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(string customerId)
        {
            // Truy cập vào database
            // 1. Khai báo thông tin database
            var connectionString = "Host = 47.241.69.179;" +
                "Database =  MF955_DuyLe_CukCuk;" +
                "User Id = dev;" +
                "Password = 12345678";

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
        /// <summary>
        /// Tìm kiếm nhân viên theo các tiêu chí
        /// </summary>
        /// <param name="filterName">Truyền vào tên, số điện thoại, mã nhân viên</param>
        /// <param name="positionId">id vị trí phòng ban</param>
        /// <param name="departmentId">id chức vụ</param>
        /// <returns></returns>
        /// Created by duylv - 10/08/2021
        [HttpGet("fillter")]
        public IActionResult GetCustomerByFilter([FromQuery] string filterName)
        {
            var connectionString = "Host = 47.241.69.179;" +
                 "Database = MF955_DuyLe_CukCuk;" +
                 "User Id = dev;" +
                 "Password = 12345678";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            DynamicParameters dynamicParameters = new DynamicParameters();

            var input = filterName == null ? string.Empty : filterName;
            dynamicParameters.Add("@filterName", input);
            var sqlCommand = "SELECT * FROM Customer e WHERE (e.CustomerCode LIKE CONCAT('%',@filterName,'%') " +
                "OR e.FullName LIKE CONCAT('%',@filterName,'%')" +
                "OR e.PhoneNumber LIKE CONCAT('%',@filterName,'%'))";

            var rowEffects = dbConnection.Query(sqlCommand, dynamicParameters);

            var response = StatusCode(200, rowEffects);
            return response;

        }


        [HttpPost]
        public IActionResult InsertCustomer([FromBody]List<Customer> customers)
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

            // 3. Thêm dữ liệu
            var columnsName = string.Empty;
            var columnsParam = string.Empty;


            //Đọc từng property của object:
            var properties = customers.GetType().GetProperties();

            //Duyệt từng property
            foreach(var prop in properties)
            {
                // Lấy tên của prop
                var propName = prop.Name;

                // Lấy value của prop
                var propValue = prop.GetValue(customers);

                // Lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                // Thêm param tương ứng với mỗi property của đối tượng
                dynamicParameters.Add($"@{propName}", propValue);

                columnsName += $"{propName},";
                propValue += $"@{propName},";

            }
            columnsName = columnsName.Remove(columnsName.Length - 1, 1);
            columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);
            var sqlCommand = $"INSERT INTO Customer({columnsName}) VALUES({columnsParam})";
            var customer = dbConnection.Execute(sqlCommand, dynamicParameters);

            var response = StatusCode(201, customer);
            return response;
        }

        [HttpPut("{customerId}")]
        public IActionResult InsertCustomer(string customerId, [FromBody] List<Customer> customers)
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

            // 3. Thêm dữ liệu
            var columnsName = string.Empty;


            //Đọc từng property của object:
            var properties = customers.GetType().GetProperties();

            //Duyệt từng property
            foreach (var prop in properties)
            {
                // Lấy tên của prop
                var propName = prop.Name;

                // Lấy value của prop
                var propValue = prop.GetValue(customers);

                // Lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                // Thêm param tương ứng với mỗi property của đối tượng
                dynamicParameters.Add($"@{propName}", propValue);

                columnsName += $"{propName}=@{propName},";
                propValue += $",";

            }
            columnsName = columnsName.Remove(columnsName.Length - 1, 1);
            var sqlCommand = $"UPDATE Customer SET{columnsName} WHERE CustomerId={customerId}";
            var customer = dbConnection.Execute(sqlCommand, dynamicParameters);
            var response = StatusCode(200, customers);
            return response;
        }

        [HttpDelete("{customerId}")]
        public IActionResult DeleteCustomer(string customerId)
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
            dynamicParameters.Add($"@{customerId}");

            var sqlCommand = $"DELETE FROM Customer WHERE CustomerId={customerId}";
            var customer = dbConnection.Execute(sqlCommand, dynamicParameters);

            var response = StatusCode(200, sqlCommand);
            return response;
        }
    }
}
