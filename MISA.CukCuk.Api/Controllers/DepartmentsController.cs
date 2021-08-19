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
    public class DepartmentsController : Controller
    {

        // GET, POST, PUT, DELETE
        /// <summary>
        /// Lấy toàn bộ dữ liệu 
        /// </summary>
        /// <returns>Trả về danh sách Nhóm Phòng ban</returns>
        /// Created by - duylv - 11/08/2021
        [HttpGet]
        public IActionResult GetDepartments()
        {
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MF955_DuyLe_CukCuk;" +
                "User Id = dev;" +
                "Password = 12345678";

            // 2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3. Lấy dữ liệu
            var sqlCommand = "SELECT * FROM Department";
            var departments = dbConnection.Query<Department>(sqlCommand);

            // Trả về cho client
            var response = StatusCode(200, departments);
            return response;
        }

        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// <returns>Trả về phòng ban theo id</returns>
        /// Created by - duylv - 11/08/2021
        [HttpGet("{DepartmentId}")]
        public IActionResult GetDepartmentById(Guid departmentId)
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
            parameters.Add("@DepartmentId", departmentId);
            var sqlCommand = $"SELECT * FROM Department WHERE DepartmentId = @DepartmentId";


            var Departments = dbConnection.QueryFirstOrDefault<Department>(sqlCommand, parameters);

            var response = StatusCode(200, Departments);
            return response;
        }

        [HttpGet("fillter")]
        public IActionResult GetDepartmentByFilter([FromQuery] string departmentCode)
        {
            var connectionString = "Host = 47.241.69.179;" +
                 "Database = MF955_DuyLe_CukCuk;" +
                 "User Id = dev;" +
                 "Password = 12345678";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@DepartmentCode", departmentCode);
           

            var sqlCommand = $"SELECT * FROM Department WHERE DepartmentCode = %@DepartmentCode%";


            var rowEffects = dbConnection.Query(sqlCommand, dynamicParameters);

            var response = StatusCode(200, rowEffects);
            return response;

        }

        /// <summary>
        /// Thêm mới phòng ban
        /// </summary>
        /// <returns>Trả về phòng ban thêm mới</returns>
        /// Created by - duylv - 11/08/2021
        [HttpPost]
        public IActionResult InsertDepartment([FromBody] Department departments)
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
            var properties = departments.GetType().GetProperties();
            departments.DepartmentId = Guid.NewGuid();

            //Duyệt từng property
            foreach (var prop in properties)
            {
                // Lấy tên của prop
                var propName = prop.Name;

                // Lấy value của prop
                var propValue = prop.GetValue(departments);

                // Lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                // Thêm param tương ứng với mỗi property của đối tượng
                dynamicParameters.Add($"@{propName}", propValue);

                columnsName += $"{propName},";
                columnsParam += $"@{propName},";

            }
            columnsName = columnsName.Remove(columnsName.Length - 1, 1);
            columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);
            var sqlCommand = $"INSERT INTO Department({columnsName}) VALUES({columnsParam})";
            var rowEffects = dbConnection.Execute(sqlCommand, param: dynamicParameters);

            var response = StatusCode(201, rowEffects);
            return response;
        }

        /// <summary>
        /// Sửa phòng ban
        /// </summary>
        /// <returns>Trả về phòng ban đã sửa</returns>
        /// Created by - duylv - 11/08/2021
        [HttpPatch("{DepartmentId}")]
        public IActionResult UpdateDepartment(Guid departmentId, [FromBody] Department departments)
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
            var properties = departments.GetType().GetProperties();
            departments.DepartmentId = departmentId;

            //Duyệt từng property
            foreach (var prop in properties)
            {
                // Lấy tên của prop
                var propName = prop.Name;

                // Lấy value của prop
                var propValue = prop.GetValue(departments);

                // Lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                // Thêm param tương ứng với mỗi property của đối tượng
                dynamicParameters.Add($"@{propName}", propValue);

                columnsName += $"{propName}=@{propName},";
                propValue += $",";

            }

            columnsName = columnsName.Remove(columnsName.Length - 1, 1);
            var sqlCommand = $"UPDATE Department SET {columnsName} WHERE DepartmentId=@DepartmentId";
            var department = dbConnection.Execute(sqlCommand, param: dynamicParameters);

            var response = StatusCode(200, department);
            return response;
        }

        /// <summary>
        /// Xóa phòng ban
        /// </summary>
        /// <returns></returns>
        /// Created by - duylv - 11/08/2021
        [HttpDelete("{DepartmentId}")]
        public IActionResult DeleteDepartment(Guid departmentId)
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
            dynamicParameters.Add("@DepartmentId", departmentId);


            var sqlCommand = $"DELETE FROM Department WHERE DepartmentId=@DepartmentId";
            var department = dbConnection.Execute(sqlCommand, dynamicParameters);

            var response = StatusCode(200, department);
            return response;
        }
    }
}

