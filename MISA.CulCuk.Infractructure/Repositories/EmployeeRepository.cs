using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CulCuk.Infractructure.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infractructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Contructor
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Method
        /// <summary>
        /// Thực hiện lọc và phân trang
        /// </summary>
        /// <param name="filterName">Tìm kiếm theo tên mã và số điện thoại</param>
        /// <param name="positionId">Tìm kiếm theo vị trí</param>
        /// <param name="departmentId">Tìm kếm theo phòng ban</param>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang</param>
        /// <param name="pageIndex">Vị trí của trang</param>
        /// <returns>Tổng số trang, tổng số bản ghi,  vad data</returns>
        /// CreateBy duylv - 19/08/2021
        public Paging GetEmployeePaging(string filterName, Guid? positionId, Guid? departmentId, int pageSize, int pageIndex)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            var input = filterName == null ? string.Empty : filterName;
            dynamicParameters.Add("@EmployeeFilter", input);
            dynamicParameters.Add("@PositionId", positionId);
            dynamicParameters.Add("@DepartmentId", departmentId);

            dynamicParameters.Add("@PageIndex", pageIndex);
            dynamicParameters.Add("@PageSize", pageSize);
            dynamicParameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dynamicParameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);


            var resPaging = _dbConnection.Query("Proc_GetEmployeeFilterPaging", dynamicParameters, commandType: CommandType.StoredProcedure);

            
            Paging _paging = new Paging();
            _paging.Data = resPaging.ToList();
            _paging.TotalPage = dynamicParameters.Get<Int32>("@TotalPage");
            _paging.TotalRecord = dynamicParameters.Get<Int32>("@TotalRecord");

            return _paging;
        }

        /// <summary>
        /// Xóa danh sách nhân viên
        /// </summary>
        /// <param name="listId">danh sách nhân viên</param>
        /// <returns>Số hàng xóa được</returns>
        /// CreateBy duylv - 16/08/2021
        public int DeleteListId(List<Guid> listId)
        {
            var transaction = _dbConnection.BeginTransaction();
            DynamicParameters parameters = new DynamicParameters();
            var rowEffects = 0;
            foreach (var item in listId)
            {
                parameters.Add("@EmployeeId", item.ToString());
                rowEffects += _dbConnection.Execute($"Proc_DeleteEmployee", parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
            }
            transaction.Commit();
            return rowEffects;
        }

        /// <summary>
        /// Kiểm tra trùng theo mã, sđt, email
        /// </summary>
        /// <param name="employeeId">Id nhân viên</param>
        /// <param name="input">mã, sđt, email</param>
        /// <returns>true là trùng, false là không trùng</returns>
        /// CreateBy duylv - 16/08/2021
        public bool CheckDuplicate(Guid? employeeId, string input)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@employeeCode", input);
            dynamicParameters.Add("@PhoneNumber", input);
            dynamicParameters.Add("@Email", input);


            var sqlCommand = "SELECT * FROM Employee WHERE EmployeeCode = @employeeCode " +
                "OR PhoneNumber = @PhoneNumber OR Email = @Email LIMIT 1";

            var res = _dbConnection.QueryFirstOrDefault<Employee>(sqlCommand, dynamicParameters);

            if (res != null && res.EmployeeId != employeeId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy mã có ngày thêm vào mới nhất
        /// </summary>
        /// <returns>Trả về mã có ngày mới nhất</returns>
        /// CreateBy duylv - 21/08/2021
        public string NewCode()
        {
            var sqlCommand = "SELECT EmployeeCode FROM Employee ORDER BY CreatedDate DESC LIMIT 1";
            var res = _dbConnection.QueryFirstOrDefault<string>(sqlCommand);
            return res;
        }
        #endregion
    }
}
