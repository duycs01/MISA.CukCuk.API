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
            var sqlCommand = "SELECT * FROM Employee  ORDER BY ModifiedDate DESC";
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

        /// <summary>
        /// Tìm kiếm nhân viên theo các tiêu chí
        /// </summary>
        /// <param name="filterName">Truyền vào tên, số điện thoại, mã nhân viên</param>
        /// <param name="positionId">id vị trí phòng ban</param>
        /// <param name="departmentId">id chức vụ</param>
        /// <returns></returns>
        /// Created by duylv - 10/08/2021
        [HttpGet("fillter")]
        public IActionResult GetEmployeeByFilter([FromQuery] string filterName, [FromQuery] string positionId, [FromQuery] string departmentId)
        {
            var connectionString = "Host = 47.241.69.179;" +
                 "Database = MF955_DuyLe_CukCuk;" +
                 "User Id = dev;" +
                 "Password = 12345678";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            DynamicParameters dynamicParameters = new DynamicParameters();

            var input = filterName == null ? string.Empty : filterName;
            dynamicParameters.Add("@filterName", input);
            dynamicParameters.Add("@positionId", positionId);
            dynamicParameters.Add("@departmentId", departmentId);
            var sqlCommand = "SELECT * FROM Employee e WHERE (e.EmployeeCode LIKE CONCAT('%',@filterName,'%') " +
                "OR e.FullName LIKE CONCAT('%',@filterName,'%')" +
                "OR e.PhoneNumber LIKE CONCAT('%',@filterName,'%'))" +
                "AND ((@departmentId IS NOT NULL AND e.DepartmentId = @departmentId) OR @departmentId IS NULL)" +
                "AND ((@positionId IS NOT NULL AND e.PositionId = @positionId) OR @positionId IS NULL)";


            var rowEffects = dbConnection.Query(sqlCommand, dynamicParameters);

            var response = StatusCode(200, rowEffects);
            return response;

        }


        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employees">Dữ liệu truyền vào từ body</param>
        /// <returns></returns>
        /// Created by duylv - 08/08/2021
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employees)
        {
            var res = CheckDuplicate(employees.EmployeeCode);
            if (res)
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
                employees.CreatedDate = DateTime.Now;
                employees.ModifiedDate = DateTime.Now;

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

                var response = StatusCode(201, rowEffects + "Đã thêm thành công");
                return response;
            }
            else
            {
                return StatusCode(400, "Mã nhân viên đã bị trùng");
            }
        }

        /// <summary>
        /// Sửa thông tin nhân viên
        /// </summary>
        /// <param name="employeeId">Id của nhân viên</param>
        /// <param name="employees">Nội dung cần sửa truyền từ body</param>
        /// <returns></returns>
        /// Created duylv 08/08/2021
        [HttpPatch("{employeeId}")]
        public IActionResult UpdateEmployee(Guid employeeId, [FromBody] Employee employees)
        {
            var res = CheckDuplicate(employees.EmployeeCode);
            if (res)
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
                employees.ModifiedDate = DateTime.Now;

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

                }

                columnsName = columnsName.Remove(columnsName.Length - 1, 1);
                var sqlCommand = $"UPDATE Employee SET {columnsName} WHERE EmployeeId=@EmployeeId";
                var employee = dbConnection.Execute(sqlCommand, param: dynamicParameters);

                var response = StatusCode(200, employee);
                return response;
            }
            else
            {
                return StatusCode(400, "Mã nhân viên đã bị trùng");
            }

        }

        /// <summary>
        /// Xóa nhân viên theo Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
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


        public bool CheckDuplicate(string employeeCode)
        {
            var connectionString = "Host = 47.241.69.179;" +
                    "Database = MF955_DuyLe_CukCuk;" +
                    "User Id = dev;" +
                    "Password = 12345678";

            IDbConnection dbConnection = new MySqlConnection(connectionString);

            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@employeeCode", employeeCode);

            var sqlCommand = "SELECT EmployeeCode FROM Employee WHERE EmployeeCode = @employeeCode";

            var res = dbConnection.QueryFirstOrDefault(sqlCommand, dynamicParameters);
            if (res != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        [HttpGet("checkDuplicate")]
        public IActionResult CheckEmployeeCode([FromQuery] string employeeCode)
        {
            var res = CheckDuplicate(employeeCode);
            if (res)
            {
                return StatusCode(200, "OK");
            }
            else
            {
                return StatusCode(400, "Mã nhân viên đã bị trùng");
            }

        }
    }

}
