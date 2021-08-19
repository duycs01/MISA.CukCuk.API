using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CukCuk.Core.Interfaces.Services;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
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
        public CustomersController(ICustomerServices customerServices, ICustomerRepository customerRepository) : base(customerServices, customerRepository)
        {
            _customerServices = customerServices;
        }

        //[HttpGet("paging")]
        //public override IActionResult GetAll([FromQuery] int pageSize, [FromQuery] int pageIndex)
        //{
        //    var resTotal = _customerServices.GetAll();
        //    var totalPage = 0;
        //    if (resTotal / pageSize >= 1)
        //    {
        //        totalPage = resTotal / pageSize;
        //    }
        //    else
        //    {
        //        totalPage = (resTotal / pageSize) + 1;
        //    }
        //    var res = new
        //    {
        //        data = resPaging,
        //        totalRecord = resTotal,
        //        totalPage = totalPage

        //    };

        //    return res;
        //}

    }
}
