﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entity
{
    public class CustomerGroup:BaseEntity
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid CustomerGroupId { get; set; }

        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        public string CustomerGroupName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}