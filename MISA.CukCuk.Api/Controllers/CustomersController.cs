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
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    /// <summary>
    /// API danh mục khách hàng
    /// Created by: duylv - 16/08/2021
    /// </summary>
    public class CustomersController : BaseEntityController<Customer>
    {
        ICustomerServices _customerServices;
        ICustomerRepository _customerRepository;

        public CustomersController(ICustomerServices customerServices, ICustomerRepository customerRepository) : base(customerServices, customerRepository)
        {
            _customerServices = customerServices;
            _customerRepository = customerRepository;
        }

        [HttpGet("paging")]
        public IActionResult GetCustomerPaging([FromQuery] string filterName, [FromQuery] Guid customerGroupId, [FromQuery] int pageSize, [FromQuery] int pageIndex)
        {
            try
            {
                var res = _customerRepository.GetCustomerPaging(filterName, customerGroupId, pageSize, pageIndex);
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
        [HttpDelete]
        public IActionResult DeleteListId([FromBody] List<Guid> listId)
        {
            try
            {
                var res = _customerRepository.DeleteListId(listId);

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
        /// Thêm file excel và trả tra danh sách và lỗi
        /// </summary>
        /// <param name="formFile">fifle excel</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Danh sách bản ghi kèm theo lỗi</returns>
        /// Created by: duylv - 21/08/2021
        [HttpPost("import")]
        public IActionResult ImPort(IFormFile formFile, CancellationToken cancellationToken)
        {
            try
            {
                var Customer = new List<Customer>();
                var res = _customerServices.Import(formFile, cancellationToken);
                return StatusCode(200, res);
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
        /// Thêm danh sách khách hàng vào database
        /// </summary>
        /// <param name="customers">Danh sách khách hàng</param>
        /// <returns>Số lượng bản ghi được thêm vào</returns>
        /// Created by: duylv - 21/08/2021
        [HttpPost("importData")]
        public  IActionResult ImportData([FromBody] List<Customer> customers)
        {
            try
            {
                var Customer = new List<Customer>();
                var importTrue = _customerServices.ImportData(customers);
                var importFalse = customers.Count - importTrue;
                var res = new { ImportTrue = importTrue, ImportFalse = importFalse };
                return StatusCode(200, res);
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
        /// Lấy mã khách hàng mới
        /// </summary>
        /// <returns>Mã khách hàng mới</returns>
        /// Created by: duylv - 22/08/2021
        [HttpGet("newCustomerCode")]
        public IActionResult GetNewCustomerCode()
        {
            try
            {
                var res = _customerRepository.NewCode();
                if (res != null)
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
