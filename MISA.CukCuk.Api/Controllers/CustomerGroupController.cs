using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Entity;
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
    public class CustomerGroupController : ControllerBase
    {

        // GET, POST, PUT, DELETE
        /// <summary>
        /// Lấy toàn bộ dữ liệu 
        /// </summary>
        /// <returns>Trả về danh sách Nhóm khách hàng</returns>
        /// Created by - duylv - 11/08/2021
        [HttpGet]
        public IActionResult GetCustomerGroup()
        {
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MF955_DuyLe_CukCuk;" +
                "User Id = dev;" +
                "Password = 12345678";

            // 2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3. Lấy dữ liệu
            var sqlCommand = "SELECT * FROM CustomerGroup";
            var customerGroup = dbConnection.Query<CustomerGroup>(sqlCommand);

            // Trả về cho client
            var response = StatusCode(200, customerGroup);
            return response;
        }

        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// <returns>Trả về danh sách Nhóm khách hàng</returns>
        /// Created by - duylv - 11/08/2021
        [HttpGet("{CustomerGroupId}")]
        public IActionResult GetCustomerGroupById(Guid customerGroupId)
        {
            // Truy cập vào database
            // 1. Khai báo thông tin database
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MF955_DuyLe_CukCuk;" +
                "User Id = dev;" +
                "Password = 12345678";

            // 2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3. Lấy dữ liệu
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerGroupId", customerGroupId);
            var sqlCommand = $"SELECT * FROM CustomerGroup WHERE CustomerGroupId = @CustomerGroupId";


            var customerGroup = dbConnection.QueryFirstOrDefault<CustomerGroup>(sqlCommand, parameters);

            var response = StatusCode(200, customerGroup);
            return response;
        }

        /// <summary>
        /// Thêm mới nhóm khách hàng
        /// </summary>
        /// <returns>Trả về Nhóm khách hàng được thêm mới</returns>
        /// Created by - duylv - 11/08/2021
        [HttpPost]
        public IActionResult InsertCustomerGroup([FromBody] CustomerGroup customerGroup)
        {
            // Truy cập vào database
            // 1. Khai báo thông tin database
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MF955_DuyLe_CukCuk;" +
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
            var properties = customerGroup.GetType().GetProperties();
            customerGroup.CustomerGroupId = Guid.NewGuid();

            //Duyệt từng property
            foreach (var prop in properties)
            {
                // Lấy tên của prop
                var propName = prop.Name;

                // Lấy value của prop
                var propValue = prop.GetValue(customerGroup);

                // Lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                // Thêm param tương ứng với mỗi property của đối tượng
                dynamicParameters.Add($"@{propName}", propValue);

                columnsName += $"{propName},";
                columnsParam += $"@{propName},";

            }
            columnsName = columnsName.Remove(columnsName.Length - 1, 1);
            columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);
            var sqlCommand = $"INSERT INTO CustomerGroup({columnsName}) VALUES({columnsParam})";
            var rowEffects = dbConnection.Execute(sqlCommand, param: dynamicParameters);

            var response = StatusCode(201, rowEffects);
            return response;
        }

        [HttpPatch("{CustomerGroupId}")]
        public IActionResult UpdateCustomerGroup(Guid customerGroupId, [FromBody] CustomerGroup customerGroup)
        {
            // Truy cập vào database
            // 1. Khai báo thông tin database
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MF955_DuyLe_CukCuk;" +
                "User Id = dev;" +
                "Password = 12345678";

            // 2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            // Khai báo Dynamic Param
            DynamicParameters dynamicParameters = new DynamicParameters();

            // 3. Thêm dữ liệu
            var columnsName = string.Empty;


            //Đọc từng property của object:
            var properties = customerGroup.GetType().GetProperties();
            customerGroup.CustomerGroupId = customerGroupId;

            //Duyệt từng property
            foreach (var prop in properties)
            {
                // Lấy tên của prop
                var propName = prop.Name;

                // Lấy value của prop
                var propValue = prop.GetValue(customerGroup);

                // Lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                // Thêm param tương ứng với mỗi property của đối tượng
                dynamicParameters.Add($"@{propName}", propValue);

                columnsName += $"{propName}=@{propName},";

            }

            columnsName = columnsName.Remove(columnsName.Length - 1, 1);
            var sqlCommand = $"UPDATE CustomerGroup SET {columnsName} WHERE CustomerGroupId=@CustomerGroupId";
            var customerGroups = dbConnection.Execute(sqlCommand, param: dynamicParameters);

            var response = StatusCode(200, customerGroups);
            return response;
        }

        /// <summary>
        /// Xóa nhóm khách hàng
        /// </summary>
        /// <returns></returns>
        /// Created by - duylv - 11/08/2021
        [HttpDelete("{CustomerGroupId}")]
        public IActionResult DeleteCustomerGroup(Guid customerGroupId)
        {
            // Truy cập vào database
            // 1. Khai báo thông tin database
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MF955_DuyLe_CukCuk;" +
                "User Id = dev;" +
                "Password = 12345678";

            // 2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            // Khai báo Dynamic Param
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@CustomerGroupId", customerGroupId);


            var sqlCommand = $"DELETE FROM CustomerGroup WHERE CustomerGroupId=@CustomerGroupId";
            var CustomerGroup = dbConnection.Execute(sqlCommand, dynamicParameters);

            var response = StatusCode(200, CustomerGroup);
            return response;
        }
    }
}
