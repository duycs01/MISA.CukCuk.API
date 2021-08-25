using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CukCuk.Core.Interfaces.Services;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerGroupController : BaseEntityController<CustomerGroup>
    {
        ICustomerGroupServices _customerGroupServices;
        ICustomerGroupRepository _customerGroupRepository;

        public CustomerGroupController(ICustomerGroupServices customerGroupServices, ICustomerGroupRepository customerGroupRepository) : base(customerGroupServices, customerGroupRepository)
        {
            _customerGroupServices = customerGroupServices;
            _customerGroupRepository = customerGroupRepository;
        }
    }
}
