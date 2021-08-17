using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CukCuk.Core.MISAAttribute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CulCuk.Infractructure.Repositories
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
        #region DECLEAR
        IConfiguration _configuration;
        string _connectionString = string.Empty;
        IDbConnection _dbConnection = null;
        string _tableName;
        #endregion

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="configuration"></param>
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISACukCukConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
             _tableName = typeof(MISAEntity).Name;

        }
        #region Method

        public int DeleteById(Guid entityId)
        {

            // Khai báo Dynamic Param
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{_tableName}Id", entityId);
            var sqlCommand = $"DELETE FROM {_tableName} WHERE {_tableName}Id=@{_tableName}Id";
            var res = _dbConnection.Execute(sqlCommand, dynamicParameters);
            return res;
        }

        public List<MISAEntity> GetAll()
        {
            var sqlCommand = $"SELECT * FROM {_tableName}";
            var res = _dbConnection.Query<MISAEntity>(sqlCommand);
            return (List<MISAEntity>)res;
        }

        public MISAEntity GetById(Guid entityId)
        {
            var _tableName = typeof(MISAEntity).Name;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{_tableName}Id", entityId);
            var sqlCommand = $"SELECT * FROM {_tableName}";
            var res = _dbConnection.QueryFirstOrDefault(sqlCommand);
            return res;
        }

        public int Insert(MISAEntity entity)
        {
            var properties = entity.GetType().GetProperties();
            properties[0].SetValue(entity, Guid.NewGuid()); 
            var parameters = MappingDBType(entity);
            var res = _dbConnection.Execute($"Proc_Insert{_tableName}", parameters,commandType:CommandType.StoredProcedure);
            return res;
        }

        public int Update(MISAEntity entity)
        {
            var parameters = MappingDBType(entity);
            var res = _dbConnection.Execute($"Proc_Update{_tableName}", parameters);
            return res;
        }

        private DynamicParameters MappingDBType(MISAEntity entity)
        {
            //Đọc từng property của object:
            var properties = entity.GetType().GetProperties();
            DynamicParameters dynamicParameters = new DynamicParameters();

            //Duyệt từng property
            foreach (var prop in properties)
            {
                var propAttributeNotMap = prop.GetCustomAttributes(typeof(MISANotMap), true);
                if (propAttributeNotMap.Length == 0)
                {
                    // Lấy tên của prop
                    var propName = prop.Name;

                    // Lấy value của prop
                    var propValue = prop.GetValue(entity);

                    // Lấy kiểu dữ liệu của prop
                    var propType = prop.PropertyType;

                    // Thêm param tương ứng với mỗi property của đối tượng
                    dynamicParameters.Add($"@{propName}", propValue);

                }
            }
            return dynamicParameters;
        }
        #endregion

    }

}
