using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Api.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
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
                var response = StatusCode(200, customers);
                return response;
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,

                    errorCode = "misa-001",
                    //moreInfor = "...",
                    //traceId = "",
                };
                return StatusCode(500, errObj);
            }
            
        }


        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(Guid customerId)
        {
            try
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
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,

                    errorCode = "misa-001",
                    //moreInfor = "...",
                    //traceId = "",
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
        public IActionResult GetCustomerByFilter([FromQuery] string filterName, [FromQuery] Guid? positionId, [FromQuery] Guid? departmentId)
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
                var sqlCommand = "SELECT * FROM Customer e WHERE (e.CustomerCode LIKE CONCAT('%',@filterName,'%') " +
                    "OR e.FullName LIKE CONCAT('%',@filterName,'%')" +
                    "OR e.PhoneNumber LIKE CONCAT('%',@filterName,'%'))" +
                    "AND ((@departmentId IS NOT NULL AND e.DepartmentId = @departmentId) OR @departmentId IS NULL)" +
                    "AND ((@positionId IS NOT NULL AND e.PositionId = @positionId) OR @positionId IS NULL)";


                var res = dbConnection.Query(sqlCommand, dynamicParameters);

                if (res.Count() > 0)
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


        [HttpPost]
        public IActionResult InsertCustomer([FromBody]Customer customers)
        {
            try
            {
                // Mã nhân viên bắt buộc phải có
                if (customers.CustomerCode == "" || customers.CustomerCode == null)
                {
                    var errObj = new
                    {
                        devMsg = Properties.Resources.EmtyNull_CustomerCode,
                        userMsg = Properties.Resources.EmtyNull_CustomerCode,
                        //errorCode = "misa-001",
                        //moreInfor = "...",
                        //traceId = "1232",
                    };
                    return BadRequest(errObj);
                }

                // Kiểm tra trùng mã
                var customerCode = checkDuplicate(customers.CustomerCode);
                if (customerCode == true)
                {
                    var errObj = new
                    {
                        devMsg = Properties.Resources.Duplicate_CustomerCode,
                        userMsg = Properties.Resources.Duplicate_CustomerCode,
                        //errorCode = "misa-001",
                        //moreInfor = "...",
                        //traceId = "1232",
                    };
                    return StatusCode(400, errObj);
                }

                var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                bool isValid = Regex.IsMatch(customers.Email, regex, RegexOptions.IgnoreCase);
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
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,

                    errorCode = "misa-001",
                    //moreInfor = "...",
                    //traceId = "",
                };
                return StatusCode(500, errObj);
            }
            // Truy cập vào database
            // 1. Khai báo thông tin database
            
        }

        [HttpPut("{customerId}")]
        public IActionResult InsertCustomer(string customerId, [FromBody] Customer customers)
        {
            try
            {
                // Mã nhân viên bắt buộc phải có
                if (customers.CustomerCode == "" || customers.CustomerCode == null)
                {
                    var errObj = new
                    {
                        devMsg = Properties.Resources.EmtyNull_CustomerCode,
                        userMsg = Properties.Resources.EmtyNull_CustomerCode,
                        //errorCode = "misa-001",
                        //moreInfor = "...",
                        //traceId = "1232",
                    };
                    return BadRequest(errObj);
                }

                // Kiểm tra trùng mã
                var customerCode = checkDuplicate(customers.CustomerCode);
                if (customerCode == true)
                {
                    var errObj = new
                    {
                        devMsg = Properties.Resources.Duplicate_CustomerCode,
                        userMsg = Properties.Resources.Duplicate_CustomerCode,
                        //errorCode = "misa-001",
                        //moreInfor = "...",
                        //traceId = "1232",
                    };
                    return StatusCode(400, errObj);
                }

                var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                bool isValid = Regex.IsMatch(customers.Email, regex, RegexOptions.IgnoreCase);
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
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,

                    errorCode = "misa-001",
                    //moreInfor = "...",
                    //traceId = "",
                };
                return StatusCode(500, errObj);
            }
            
        }

        [HttpDelete("{customerId}")]
        public IActionResult DeleteCustomer(string customerId)
        {
            try
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
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.Exception_ErrorMsg,

                    errorCode = "misa-001",
                    //moreInfor = "...",
                    //traceId = "",
                };
                return StatusCode(500, errObj);
            }
        }
            
        /// <summary>
        /// Kiểm tra trùng mã Khách hàng
        /// </summary>
        /// <param name="customerCode">Truyền vào mã Khách hàng</param>
        /// <returns>true/false</returns>
        /// CreateBy duylv - 12/08/2021
        static bool checkDuplicate(string customerCode)
        {
            var connectionString = "Host = 47.241.69.179;" +
                     "Database = MF955_DuyLe_CukCuk;" +
                     "User Id = dev;" +
                     "Password = 12345678";

            IDbConnection dbConnection = new MySqlConnection(connectionString);

            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@customerCode", customerCode);

            var sqlCommand = "SELECT customerCode FROM Customer WHERE CustomerCode = @customerCode";

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
    }
}
