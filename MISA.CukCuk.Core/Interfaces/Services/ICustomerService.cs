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

        /// <summary>
        /// Thêm file excel và trả ra lỗi
        /// </summary>
        /// <param name="formFile">File excel</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Trả ra danh sách các bản ghi cảu file excel kèm theo lỗi gặp phải</returns>
        /// Created by: duylv - 23/08/2021
        ServiceResult Import(IFormFile formFile, CancellationToken cancellationToken);

        /// <summary>
        /// Thêm danh sách khách hàng vào data base
        /// </summary>
        /// <param name="customers">Danh sách khách hàng</param>
        /// <returns>Số bản ghi được thêm vào</returns>
        /// Created by: duylv - 22/08/2021
        int ImportData(List<Customer> customers);
    }
}
