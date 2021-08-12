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
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace MISA.CukCuk.Api.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        // GET, POST, PUT, DELETE
        /// <summary>
        /// Lấy toàn bộ dữ liệu nhân viên
        /// </summary>
        /// <returns>Trả về danh sách nhân viên</returns>
        /// Created by - duylv - 11/08/2021
        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
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
                var sqlCommand = "SELECT * FROM Employee  ORDER BY ModifiedDate DESC";
                var employees = dbConnection.Query<Employee>(sqlCommand);

                // Trả về cho client
                if (employees.Count() > 0)
                {
                    return StatusCode(200, employees);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,
                    //errorCode = "misa-001",
                    //moreInfor = "...",
                };
                return StatusCode(500, errObj);
            }

        }

        /// <summary>
        /// Thực hiện phân trang 
        /// </summary>
        /// <param name="pageSize">Số lượng dữ liệu hiển thị trên một trang</param>
        /// <param name="pageIndex">Vị trí trang được chọn</param>
        /// <returns>Trả về dữ liệu đã được phân trang</returns>
        /// Created by - duylv - 12/08/2021
        /// 
        [HttpGet("paging")]
        public IActionResult getEmployeesByPaging([FromQuery] int pageSize, [FromQuery] int pageIndex)
        {

            try
            {
                var connectionString = "Host = 47.241.69.179;" +
                   "Database = MF955_DuyLe_CukCuk;" +
                   "User Id = dev;" +
                   "Password = 12345678";
                IDbConnection dbConnection = new MySqlConnection(connectionString);
                DynamicParameters dynamicParameters = new DynamicParameters();

                dynamicParameters.Add("@OffsetParam", (pageIndex - 1) * pageSize);
                dynamicParameters.Add("@LimitParam", pageSize);

                var sqlCommandPaging = "SELECT * FROM Employee LIMIT @LimitParam OFFSET @OffsetParam";
                var sqlCommandCount = "SELECT COUNT(EmployeeId) FROM Employee";

                var resPaging = dbConnection.Query<Employee>(sqlCommandPaging, param: dynamicParameters);
                var resTotal = dbConnection.QueryFirst<int>(sqlCommandCount);

                var totalPage = 0;
                if (resTotal / pageSize >= 1)
                {
                    totalPage = resTotal / pageSize;
                }
                else
                {
                    totalPage = (resTotal / pageSize) + 1;
                }
                var res = new
                {
                    data = resPaging,
                    totalRecord = resTotal,
                    totalPage = totalPage

                };
                if (res.data.Count() > 0)
                {
                    return StatusCode(200, res);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,
                    //errorCode = "misa-001",
                    //moreInfor = "...",
                };
                return StatusCode(500, errObj);
            }

        }

        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// <param name="employeeId">Truyền vào id cần lấy dữ liệu</param>
        /// <returns>Trả ra kết quả lấy được theo id</returns>
        /// Created by - duylv - 10/08/2021
        /// 
        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeById(Guid employeeId)
        {
            try
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

                if (employees != null)
                {
                    return StatusCode(200, employees);
                }
                else
                {
                    return StatusCode(204);
                }
            }

            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,
                    //errorCode = "misa-001",
                    //moreInfor = "...",
                };
                return StatusCode(500, errObj);
            }
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
        public IActionResult GetEmployeeByFilter([FromQuery] string filterName, [FromQuery] Guid? positionId, [FromQuery] Guid? departmentId)
        {
            try
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


            var res = dbConnection.Query(sqlCommand, dynamicParameters);

                if (res.Count()>0)
                {
                    return StatusCode(200, res);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,
                    //errorCode = "misa-001",
                    //moreInfor = "...",
                };
                return StatusCode(500, errObj);
            }
            

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
            
            try
            {
                
                // Mã nhân viên bắt buộc phải có
                if (employees.EmployeeCode == "" || employees.EmployeeCode == null)
                {
                    var errObj = new
                    {
                        devMsg = Properties.Resources.EmtyNull_EmployeeCode,
                        userMsg = Properties.Resources.EmtyNull_EmployeeCode,
                        //errorCode = "misa-001",
                        //moreInfor = "...",
                        //traceId = "1232",
                    };
                    return BadRequest(errObj);
                }

                // Kiểm tra trùng mã
                var employeeCode = checkDuplicate(employees.EmployeeCode);
                if (employeeCode == true)
                {
                    var errObj = new
                    {
                        devMsg = Properties.Resources.Duplicate_EmployeeCode,
                        userMsg = Properties.Resources.Duplicate_EmployeeCode,
                        //errorCode = "misa-001",
                        //moreInfor = "...",
                        //traceId = "1232",
                    };
                    return StatusCode(400, errObj);
                }

                var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                bool isValid = Regex.IsMatch(employees.Email, regex, RegexOptions.IgnoreCase);
                if (isValid)
                {
                    var errObj = new
                    {
                        devMsg = Properties.Resources.Error_Email,
                        userMsg = Properties.Resources.Error_Email,
                        //errorCode = "misa-001",
                        //moreInfor = "...",
                        //traceId = "1232",
                    };
                    return StatusCode(400, errObj);
                }
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
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi sảy ra! Vui lòng liên hệ với MISA",

                    errorCode = "misa-001",
                    //moreInfor = "...",
                    //traceId = "",
                };
                return StatusCode(500, errObj);
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
            try
            {
                // Mã nhân viên bắt buộc phải có
                if (employees.EmployeeCode == "" || employees.EmployeeCode == null)
                {
                    var errObj = new
                    {
                        devMsg = Properties.Resources.EmtyNull_EmployeeCode,
                        userMsg = Properties.Resources.EmtyNull_EmployeeCode,
                        //errorCode = "misa-001",
                        //moreInfor = "...",
                        //traceId = "1232",
                    };
                    return BadRequest(errObj);
                }

                // Kiểm tra trùng mã
                var employeeCode = checkDuplicate(employees.EmployeeCode);
                if (!employeeCode)
                {
                    var errObj = new
                    {
                        devMsg = Properties.Resources.Duplicate_EmployeeCode,
                        userMsg = Properties.Resources.Duplicate_EmployeeCode,
                        //errorCode = "misa-001",
                        //moreInfor = "...",
                        //traceId = "1232",
                    };
                    return StatusCode(400, errObj);
                }

                var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                bool isValid = Regex.IsMatch(employees.Email, regex, RegexOptions.IgnoreCase);
                if (isValid)
                {
                    var errObj = new
                    {
                        devMsg = Properties.Resources.Error_Email,
                        userMsg = Properties.Resources.Error_Email,
                        //errorCode = "misa-001",
                        //moreInfor = "...",
                        //traceId = "1232",
                    };
                    return StatusCode(400, errObj);
                }

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
            catch (Exception)
            {

                throw;
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

        static bool checkDuplicate(string employeeCode)
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
            if (res == null)
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
            var res = checkDuplicate(employeeCode);
            if (res ==false)
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
