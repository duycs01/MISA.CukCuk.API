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
    public class EmployeesController : ControllerBase
    {

            // GET, POST, PUT, DELETE
            /// <summary>
            /// Lấy toàn bộ dữ liệu
            /// </summary>
            /// <returns></returns>
            [HttpGet]
            public IActionResult GetEmployees()
            {
                var connectionString = "Host = 47.241.69.179;" +
                    "Database = MF955_DuyLe_CukCuk;" +
                    "User Id = dev;" +
                    "Password = 12345678";

                // 2. Khởi tạo đối tượng kết nối với database
                IDbConnection dbConnection = new MySqlConnection(connectionString);

                // 3. Lấy dữ liệu
                var sqlCommand = "SELECT * FROM Employee";
                var employees = dbConnection.Query<Employee>(sqlCommand);

                // Trả về cho client
                var response = StatusCode(200, employees);
                return response;
            }

                /// <summary>
                /// Lấy dữ liệu theo id
                /// </summary>
                /// <param name="employeeId"></param>
                /// <returns></returns>
            [HttpGet("{employeeId}")]
            public IActionResult GetEmployeeById(Guid employeeId)
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
            parameters.Add("@employeeId", employeeId);
            var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeId = @employeeId";
               

                var employees = dbConnection.QueryFirstOrDefault<Employee>(sqlCommand, parameters);

                var response = StatusCode(200, employees);
                return response;
            }

            [HttpGet("fillter")]
            public IActionResult GetEmployeeByFilter([FromQuery]string employeeCode, [FromQuery] string fullName, [FromQuery] string phoneNumber)
        {
               var connectionString = "Host = 47.241.69.179;" +
                    "Database = MF955_DuyLe_CukCuk;" +
                    "User Id = dev;" +
                    "Password = 12345678";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@employeeCode", employeeCode);
            dynamicParameters.Add( "@fullName",fullName);
            dynamicParameters.Add("@phoneNumber",phoneNumber);

            var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeCode = %@employeeCode% or FullName = %@fullName% or PhoneNumber = %@phoneNumber%";


            var rowEffects = dbConnection.Query(sqlCommand, dynamicParameters);

            var response = StatusCode(200, rowEffects);
            return response;

        }


        [HttpPost]
            public IActionResult InsertEmployee([FromBody] Employee employees)
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
                var properties = employees.GetType().GetProperties();
                employees.EmployeeId = Guid.NewGuid();

                //Duyệt từng property
            foreach (var prop in properties)
                {
                    // Lấy tên của prop
                    var propName = prop.Name;

                    // Lấy value của prop
                    var propValue = prop.GetValue(employees);

                    // Lấy kiểu dữ liệu của prop
                    var propType = prop.PropertyType;

                    // Thêm param tương ứng với mỗi property của đối tượng
                    dynamicParameters.Add($"@{propName}", propValue);

                    columnsName += $"{propName},";
                columnsParam += $"@{propName},";

                }
                columnsName = columnsName.Remove(columnsName.Length - 1, 1);
                columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);
                var sqlCommand = $"INSERT INTO Employee({columnsName}) VALUES({columnsParam})";
                var rowEffects = dbConnection.Execute(sqlCommand, param: dynamicParameters);

                var response = StatusCode(201, rowEffects);
                return response;
            }

            [HttpPatch("{employeeId}")]
            public IActionResult UpdateEmployee(Guid employeeId, [FromBody] Employee employees)
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
                var properties = employees.GetType().GetProperties();
                employees.EmployeeId = employeeId;

            //Duyệt từng property
            foreach (var prop in properties)
                {
                    // Lấy tên của prop
                    var propName = prop.Name;

                    // Lấy value của prop
                    var propValue = prop.GetValue(employees);

                    // Lấy kiểu dữ liệu của prop
                    var propType = prop.PropertyType;

                    // Thêm param tương ứng với mỗi property của đối tượng
                    dynamicParameters.Add($"@{propName}", propValue);

                    columnsName += $"{propName}=@{propName},";
                    propValue += $",";

                }

            columnsName = columnsName.Remove(columnsName.Length - 1, 1);
                var sqlCommand = $"UPDATE Employee SET {columnsName} WHERE EmployeeId=@EmployeeId";
            var employee = dbConnection.Execute(sqlCommand, param: dynamicParameters);

            var response = StatusCode(200, employee);
                return response;
            }

            [HttpDelete("{employeeId}")]
            public IActionResult DeleteEmployee(Guid employeeId)
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
            dynamicParameters.Add("@employeeId", employeeId);


            var sqlCommand = $"DELETE FROM Employee WHERE EmployeeId=@employeeId";
                var employee = dbConnection.Execute(sqlCommand, dynamicParameters);

                var response = StatusCode(200, employee);
                return response;
            }
    }
}
