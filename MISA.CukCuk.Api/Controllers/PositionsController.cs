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
    public class PositionsController : BaseEntityController<Position>
    {
        #region DECLEAR
        IPositionServices _positionServices;
        IPositionRepository _positionRepository;
        #endregion

        #region Contructor
        public PositionsController(IPositionServices positionServices, IPositionRepository positionRepository) : base(positionServices, positionRepository)
        {
            _positionServices = positionServices;
            _positionRepository = positionRepository;
        }
        #endregion

    }
}
