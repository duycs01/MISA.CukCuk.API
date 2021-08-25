using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CukCuk.Core.Interfaces.Services;
using MySqlConnector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseEntityController<Employee>
    {
        #region DECLEAR
        IEmployeeRepository _employeeRepository;
        IEmployeeServices _employeeServices;
        #endregion

        #region Contructor
        public EmployeesController(IEmployeeServices employeeServices, IEmployeeRepository employeeRepository) : base(employeeServices, employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeServices = employeeServices;
        }
        #endregion

        #region Method
        /// <summary>
        /// Phân trang và lọc nhân viên
        /// </summary>
        /// <param name="filterName">Lọc họ tên, mã nhân viên và số điện thoại</param>
        /// <param name="positionId">Lọc theo vị trí phòng ban</param>
        /// <param name="departmentId">Lọc theo phòng ban</param>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang</param>
        /// <param name="pageIndex">Vị trí của trang</param>
        /// <returns>Tổng số bản ghi, tổng sổ trang, danh sách bản ghi đã lọc và phân trang</returns>
        /// Created by: duylv - 18/08/2021
        [HttpGet("GetEmployeePaging")]
        public IActionResult GetEmployeePaging([FromQuery] string filterName, [FromQuery] Guid? positionId, [FromQuery] Guid? departmentId, [FromQuery] int pageSize, [FromQuery] int pageIndex)
        {
            try
            {

                var res = _employeeRepository.GetEmployeePaging(filterName, positionId, departmentId, pageSize, pageIndex);
                if (res.Data.Count > 0)
                {
                    return StatusCode(200, res);
                }
                else
                {
                    return StatusCode(204, res);
                }
            }
            catch (Exception ex)
            {

                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.CukCuk.Core.Resources.Resources.Exception_ErrorMsg,
                };
                _serviceResult.Data = errObj;
                return StatusCode(500, _serviceResult);
            }

        }

        /// <summary>
        /// Xóa danh sách bản ghi theo id
        /// </summary>
        /// <param name="listId">danh sách id</param>
        /// <returns>Số lượng bản ghi xóa được</returns>
        /// Created by: duylv - 17/08/2021
        [HttpPost("deleteList")]
        public IActionResult DeleteListId([FromBody] List<Guid> listId)
        {
            try
            {
                var res = _employeeRepository.DeleteListId(listId);

                if (res > 0)
                {
                    return StatusCode(200, res);
                }
                else
                {
                    return StatusCode(204, res);
                }
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.CukCuk.Core.Resources.Resources.Exception_ErrorMsg,
                };
                _serviceResult.Data = errObj;
                return StatusCode(500, _serviceResult);
            }
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Created by: duylv - 22/08/2021
        [HttpGet("newEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var res = _employeeServices.NewCode();

                if (res !=null)
                {
                    return StatusCode(200, res);
                }
                else
                {
                    return StatusCode(204, res);
                }
            }
            catch (Exception ex)
            {

                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.CukCuk.Core.Resources.Resources.Exception_ErrorMsg,
                };
                _serviceResult.Data = errObj;
                return StatusCode(500, _serviceResult);
            }
        }
        #endregion

    }
}

