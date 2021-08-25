using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface IEmployeeServices : IBaseService<Employee>
    {
        /// <summary>
        /// lấy mã nhân viên mới
        /// </summary>
        /// <returns>Trả ra mã nhân viên mới</returns>
        /// Created by: duylv - 21/08/2021
        public string NewCode();
    }
}
