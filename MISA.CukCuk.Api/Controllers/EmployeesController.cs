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
        IEmployeeRepository _employeeRepository;
        IEmployeeServices _employeeServices;
        public EmployeesController(IEmployeeServices employeeServices, IEmployeeRepository employeeRepository):base(employeeServices, employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        [HttpGet("GetEmployeePaging")]
        public IActionResult GetEmployeePaging([FromQuery] string filterName, [FromQuery] Guid? positionId, [FromQuery] Guid? departmentId, [FromQuery] int pageSise, [FromQuery] int pageIndex)
        {
            try
            {
              

                var res = _employeeRepository.GetEmployeePaging(filterName, positionId, departmentId, pageSise, pageIndex);
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
       
        [HttpDelete]
        public IActionResult DeleteListId([FromBody] dynamic listId)
        {

            try
            {
                var listEmployeeId = JObject.Parse(listId.ToString());
                var res = _employeeRepository.DeleteListId(listEmployeeId);

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

    }
    }

