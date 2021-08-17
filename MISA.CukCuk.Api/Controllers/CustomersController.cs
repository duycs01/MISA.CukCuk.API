using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Entity;
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
    /// <summary>
    /// API danh mục khách hàng
    /// Created by: duylv - 16/08/2021
    /// </summary>
    public class CustomersController : BaseEntityController<Customer>
    {
        IBaseService<Customer> _baseService;
        public CustomersController(IBaseService<Customer> baseService) :base(baseService)
        {
            _baseService = baseService;
        }
    }
}
