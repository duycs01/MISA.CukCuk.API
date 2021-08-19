using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
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
        IBaseRepository<MISAEntity> _baseRepository;

        protected ServiceResult _serviceResult;

        public BaseEntityController(IBaseService<MISAEntity> baseService, IBaseRepository<MISAEntity> baseRepository)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
        }

        /// <summary>
        /// Lấy danh sách tất cả dữ liệu
        /// </summary>
        /// <returns>Trả về danh sách dữ liệu</returns>
        /// Created by: duylv 16/08/2021
        [HttpGet]
        public virtual IActionResult GetAll()
        {
            try
            {
                var res = _baseService.GetAll();
                if (res.IsValid == true)
                {
                    return StatusCode(200, res.Data);
                }
                else
                {
                    return StatusCode(400, res.Data);
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
        /// Lấy thông tin dữ liệu theo id
        /// </summary>
        /// <returns>Trả thông tin dữ liệu</returns>
        /// Created by: duylv 16/08/2021
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var res = _baseService.GetById(id);
                if (res.IsValid == true)
                {
                    return StatusCode(200, res.Data);
                }
                else
                {
                    return StatusCode(400, res.Data);
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
        /// Thêm dữ liệu
        /// </summary>
        /// <returns>Trả về số hàng được thêm vào</returns>
        /// Created by: duylv 16/08/2021
        [HttpPost]
        public IActionResult Insert(MISAEntity entity)
        {
            try
            {
                var res = _baseService.Insert(entity);
                if (res.IsValid == true)
                {
                    return StatusCode(200, res.Data);
                }
                else
                {
                    return StatusCode(400, res.Data);
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
        /// Sửa thông tin dữ liệu theo id
        /// </summary>
        /// <returns></returns>
        /// Created by: duylv 16/08/2021
        [HttpPut("{entityId}")]
        public IActionResult Update(Guid entityId, MISAEntity entity)
        {
            try
            {
                var res = _baseService.Update(entityId, entity);
                if (res.IsValid == true)
                {
                    return StatusCode(200, res.Data);
                }
                else
                {
                    return StatusCode(400, res);
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
        /// Xóa thông tin dữ liệu theo id
        /// </summary>
        /// <returns></returns>
        /// Created by: duylv 16/08/2021
        [HttpDelete("{entityId}")]
        public IActionResult DeleteById(Guid entityId)
        {
            try
            {
               var res = _baseService.DeleteById(entityId);
                if (res.IsValid == true)
                {
                    return StatusCode(200, res.Data);
                }
                else
                {
                    return StatusCode(400, res.Data);
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
