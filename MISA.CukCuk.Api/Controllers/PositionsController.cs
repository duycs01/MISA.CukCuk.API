using Dapper;
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
    public class PositionsController : Controller
    {

        // GET, POST, PUT, DELETE
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPositions()
        {
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MF955_DuyLe_CukCuk;" +
                "User Id = dev;" +
                "Password = 12345678";

            // 2. Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3. Lấy dữ liệu
            var sqlCommand = "SELECT * FROM Position";
            var Positions = dbConnection.Query<Position>(sqlCommand);

            // Trả về cho client
            var response = StatusCode(200, Positions);
            return response;
        }

        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// <param name="PositionId"></param>
        /// <returns></returns>
        [HttpGet("{PositionId}")]
        public IActionResult GetPositionById(Guid PositionId)
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
            parameters.Add("@PositionId", PositionId);
            var sqlCommand = $"SELECT * FROM Position WHERE PositionId = @PositionId";


            var Positions = dbConnection.QueryFirstOrDefault<Position>(sqlCommand, parameters);

            var response = StatusCode(200, Positions);
            return response;
        }

        [HttpGet("fillter")]
        public IActionResult GetPositionByFilter([FromQuery] string PositionCode)
        {
            var connectionString = "Host = 47.241.69.179;" +
                 "Database = MF955_DuyLe_CukCuk;" +
                 "User Id = dev;" +
                 "Password = 12345678";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@PositionCode", PositionCode);


            var sqlCommand = $"SELECT * FROM Position WHERE PositionCode = %@PositionCode%";


            var rowEffects = dbConnection.Query(sqlCommand, dynamicParameters);

            var response = StatusCode(200, rowEffects);
            return response;

        }


        [HttpPost]
        public IActionResult InsertPosition([FromBody] Position Positions)
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
            var properties = Positions.GetType().GetProperties();
            Positions.PositionId = Guid.NewGuid();

            //Duyệt từng property
            foreach (var prop in properties)
            {
                // Lấy tên của prop
                var propName = prop.Name;

                // Lấy value của prop
                var propValue = prop.GetValue(Positions);

                // Lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                // Thêm param tương ứng với mỗi property của đối tượng
                dynamicParameters.Add($"@{propName}", propValue);

                columnsName += $"{propName},";
                columnsParam += $"@{propName},";

            }
            columnsName = columnsName.Remove(columnsName.Length - 1, 1);
            columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);
            var sqlCommand = $"INSERT INTO Position({columnsName}) VALUES({columnsParam})";
            var rowEffects = dbConnection.Execute(sqlCommand, param: dynamicParameters);

            var response = StatusCode(201, rowEffects);
            return response;
        }

        [HttpPatch("{PositionId}")]
        public IActionResult UpdatePosition(Guid PositionId, [FromBody] Position Positions)
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
            var properties = Positions.GetType().GetProperties();
            Positions.PositionId = PositionId;

            //Duyệt từng property
            foreach (var prop in properties)
            {
                // Lấy tên của prop
                var propName = prop.Name;

                // Lấy value của prop
                var propValue = prop.GetValue(Positions);

                // Lấy kiểu dữ liệu của prop
                var propType = prop.PropertyType;

                // Thêm param tương ứng với mỗi property của đối tượng
                dynamicParameters.Add($"@{propName}", propValue);

                columnsName += $"{propName}=@{propName},";
                propValue += $",";

            }

            columnsName = columnsName.Remove(columnsName.Length - 1, 1);
            var sqlCommand = $"UPDATE Position SET {columnsName} WHERE PositionId=@PositionId";
            var Position = dbConnection.Execute(sqlCommand, param: dynamicParameters);

            var response = StatusCode(200, Position);
            return response;
        }

        [HttpDelete("{PositionId}")]
        public IActionResult DeletePosition(Guid PositionId)
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
            dynamicParameters.Add("@PositionId", PositionId);


            var sqlCommand = $"DELETE FROM Position WHERE PositionId=@PositionId";
            var Position = dbConnection.Execute(sqlCommand, dynamicParameters);

            var response = StatusCode(200, Position);
            return response;
        }
    }
}
