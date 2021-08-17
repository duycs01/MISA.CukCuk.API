using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<MISAEntity> : ControllerBase
    {
        IBaseService<MISAEntity> _baseService;
        ServiceResult _serviceResult;

        public BaseEntityController(IBaseService<MISAEntity> baseService)
        {
            _baseService = baseService;
            _serviceResult = new ServiceResult();
        }

        /// <summary>
        /// Lấy danh sách tất cả dữ liệu
        /// </summary>
        /// <returns>Trả về danh sách dữ liệu</returns>
        /// Created by: duylv 16/08/2021
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _serviceResult.Data = _baseService.GetAll();
                return StatusCode(200, _serviceResult);
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.CukCuk.Core.Resources.Resources.Exception_ErrorMsg,
                };
                _serviceResult.Messenger = errObj;
                return StatusCode(500, _serviceResult);

            }
        }

        /// <summary>
        /// Lấy thông tin dữ liệu theo id
        /// </summary>
        /// <returns>Trả thông tin dữ liệu</returns>
        /// Created by: duylv 16/08/2021
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                _serviceResult.Data = _baseService.GetById(id);
                return StatusCode(200, _serviceResult);
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.CukCuk.Core.Resources.Resources.Exception_ErrorMsg,
                };
                _serviceResult.Messenger = errObj;
                return StatusCode(500, _serviceResult);

            }
        }

        /// <summary>
        /// Lấy thông tin dữ liệu theo id
        /// </summary>
        /// <returns>Trả thông tin dữ liệu</returns>
        /// Created by: duylv 16/08/2021
        [HttpPost]
        public IActionResult Insert(MISAEntity entity)
        {
            try
            {
                _serviceResult.Data = _baseService.Insert(entity);
                return StatusCode(201, _serviceResult.Data);
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.CukCuk.Core.Resources.Resources.Exception_ErrorMsg,
                };
                _serviceResult.Messenger = errObj;
                return StatusCode(500, _serviceResult);

            }
        }

        /// <summary>
        /// Sửa thông tin dữ liệu theo id
        /// </summary>
        /// <returns></returns>
        /// Created by: duylv 16/08/2021
        [HttpPut]
        public IActionResult Update(Guid id , MISAEntity entity)
        {
            try
            {
                _serviceResult.Data = _baseService.Update(id, entity);
                return StatusCode(200, _serviceResult);
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.CukCuk.Core.Resources.Resources.Exception_ErrorMsg,
                };
                _serviceResult.Messenger = errObj;
                return StatusCode(500, _serviceResult);

            }
        }

        /// <summary>
        /// Xóa thông tin dữ liệu theo id
        /// </summary>
        /// <returns></returns>
        /// Created by: duylv 16/08/2021
        [HttpDelete]
        public IActionResult DeleteById(Guid id)
        {
            try
            {
                _serviceResult.Data = _baseService.DeleteById(id);
                return StatusCode(200, _serviceResult);
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.CukCuk.Core.Resources.Resources.Exception_ErrorMsg,
                };
                _serviceResult.Messenger = errObj;
                return StatusCode(500, _serviceResult);

            }
        }
    }
}
