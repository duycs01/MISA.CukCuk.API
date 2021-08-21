using Microsoft.AspNetCore.Http;
using MISA.CukCuk.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Services
{
    public interface ICustomerServices : IBaseService<Customer>
    {
        /// <summary>
        /// Xóa danh sách thông tin khách hàng
        /// </summary>
        /// <param name="listId">danh sách id khách hàng</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteListId(List<Guid> listId);
        ServiceResult Import(IFormFile formFile, CancellationToken cancellationToken);
    }
}
