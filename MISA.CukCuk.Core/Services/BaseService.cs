﻿using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CukCuk.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.CukCuk.Core.MISAAttribute.MISAAttribute;

namespace MISA.CukCuk.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        #region DECLEAR
        IBaseRepository<MISAEntity> _baseRepository;
        protected ServiceResult _serviceResult;
        string _tableName;
        #endregion

        #region Contructor
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
            _tableName = typeof(MISAEntity).Name;

        }
        #endregion

        #region Method
        /// <summary>
        /// Xóa bản ghi theo id
        /// </summary>
        /// <param name="entityId">id bản ghi</param>
        /// <returns>Số bản ghi xóa được</returns>
        /// Created by: duylv - 14/08/2021
        public ServiceResult DeleteById(Guid entityId)
        {
            _serviceResult.Data = _baseRepository.DeleteById(entityId);
            return _serviceResult;
        }

        /// <summary>
        /// Lấy danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: duylv - 14/08/2021
        public ServiceResult GetAll()
        {
            _serviceResult.Data = _baseRepository.GetAll();
            return _serviceResult;
        }

        /// <summary>
        /// Lấy thông tin bản ghi theo id
        /// </summary>
        /// <param name="entityId">id bản ghi</param>
        /// <returns>Thông tin bản ghi</returns>
        /// Created by: duylv - 14/08/2021
        public ServiceResult GetById(Guid entityId)
        {
            _serviceResult.Data = _baseRepository.GetById(entityId);
            return _serviceResult;
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns>Số bản ghi thêm được</returns>
        /// Created by: duylv - 14/08/2021
        public ServiceResult Insert(MISAEntity entity)
        {
            var isValid = ValidateData(entity);
            if (isValid == true)
            {
                isValid = ValidateCustom(entity);
            }
            if (isValid == true)
            {
                _serviceResult.Data = _baseRepository.Insert(entity);
            }
            return _serviceResult;
        }

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="entityId">id bản ghi</param>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns>Số bản ghi được sửa</returns>
        /// Created by: duylv - 14/08/2021
        public ServiceResult Update(Guid entityId, MISAEntity entity)
        {
            var isValid = ValidateData(entity);
            var properties = entity.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name == $"{_tableName}Id")
                {
                    prop.SetValue(entity, entityId);
                }
            }
            if (isValid == true)
            {
                isValid = ValidateCustom(entity);
            }
            if (isValid == true)
            {
                _serviceResult.Data = _baseRepository.Update(entityId, entity);
            }
            return _serviceResult;
        }

        /// <summary>
        /// Sử lí validate chung
        /// </summary>
        /// <param name="entity">Đối tượng muốn validate</param>
        /// <returns>true bản ghi hợp lệ - false bản ghi không hợp lệ</returns>
        /// Created by: duylv - 16/08/2021
        protected bool ValidateData(MISAEntity entity)
        {
            // validate Bắt buộc nhập
            var properties = entity.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var propValue = prop.GetValue(entity);
                var propName = prop.Name;
                var propType = prop.PropertyType;

                var propMISARequired = prop.GetCustomAttributes(typeof(MISARequired), true);
                if (propMISARequired.Length > 0)
                {
                    var fieldName = (propMISARequired[0] as MISARequired).FieldName;
                    if (propType == typeof(string) && propValue == null || propValue.ToString() == string.Empty)
                    {
                        _serviceResult.IsValid = false;
                        _serviceResult.Messenger = $"Thông tin {fieldName} không được phép để trống";
                    }
                }
                var propMISADateTime = prop.GetCustomAttributes(typeof(MISADateTime), true);
                if (propMISADateTime.Length > 0)
                {
                    var dateNow = DateTime.Now;
                    int result = DateTime.Compare((DateTime)propValue, dateNow);
                    var fieldName = (propMISADateTime[0] as MISADateTime).FieldName;
                    if (result > 0)
                    {
                        _serviceResult.IsValid = false;
                        _serviceResult.Messenger = $"{fieldName} không được lớn hơn ngày hiện tại";
                    }
                }

            }
            return _serviceResult.IsValid;
        }

        /// <summary>
        /// Sử lí validate riêng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: duylv - 16/08/2021
        protected virtual bool ValidateCustom(MISAEntity entity)
        {
            return true;
        }
        #endregion

    }
}
