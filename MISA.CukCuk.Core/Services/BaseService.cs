using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        IBaseRepository<MISAEntity> _baseRepository;
        ServiceResult _serviceResult;
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
        }
        public ServiceResult DeleteById(Guid entityId)
        {
            _serviceResult.Data = _baseRepository.DeleteById(entityId);
            return _serviceResult;
        }

        public ServiceResult GetAll()
        {
            _serviceResult.Data = _baseRepository.GetAll();
            return _serviceResult;
        }

        public ServiceResult GetById(Guid entityId)
        {
            _serviceResult.Data = _baseRepository.GetAll();
            return _serviceResult;
        }

        public ServiceResult Insert(MISAEntity entity)
        {
            _serviceResult.Data = _baseRepository.Insert(entity);
            return _serviceResult;
        }

        public ServiceResult Update(Guid id , MISAEntity entity)
        {
            
            _serviceResult.Data = _baseRepository.Update(entity);
            return _serviceResult;
        }

        protected virtual bool ValidateCustom(MISAEntity entity)
        {
            return _serviceResult.IsValid;
        }
    }
}
