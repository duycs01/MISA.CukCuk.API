using Microsoft.AspNetCore.Http;
using MISA.CukCuk.Core.Entity;
using MISA.CukCuk.Core.Interfaces.Repository;
using MISA.CukCuk.Core.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerServices
    {
        #region DECLEAR
        ICustomerRepository _customerRepository;
        #endregion

        #region Contructor
        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #endregion

        #region Method
        public int DeleteListId(List<Guid> listId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Phân trang và lọc bản ghi
        /// </summary>
        /// <param name="filterName">Lọc họ tên, mã khách hàng, số điện thoại</param>
        /// <param name="customerGroupId">Lọc theo nhóm khách hàng</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageIndex"> Vị trí của trang </param>
        /// <returns></returns>
        /// Created by: duylv - 18/08/2021
        /// 
        public Paging GetCustomerPaging(string filterName, Guid? customerGroupId, int pageSize, int pageIndex)
        {
            return _customerRepository.GetCustomerPaging(filterName, customerGroupId, pageSize, pageIndex);
        }

        /// <summary>
        /// Thêm file excel và trả ra lỗi
        /// </summary>
        /// <param name="formFile">File excel</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Trả ra danh sách các bản ghi cảu file excel kèm theo lỗi gặp phải</returns>
        /// Created by: duylv - 22/08/2021
        /// 
        public ServiceResult Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null)
            {
                var err = new
                {
                    PropertyName = Resources.Resources.Error_ImportFile,
                    ErrorInfo = Resources.Resources.Error_ImportFile,

                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = err;
            }
            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                _serviceResult.Messenger = Resources.Resources.Error_ImportFile;

                _serviceResult.IsValid = false;
            }

            var customers = new List<Customer>();

            using (var stream = new MemoryStream())
            {
                formFile.CopyToAsync(stream, cancellationToken);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    var columnCount = worksheet.Dimension.Columns;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        var customer = new Customer();
                        for (int index = 1; index < columnCount; index++)
                        {
                            var item = worksheet.Cells[row, index].Value;
                            if (item != null)
                            {
                                switch (worksheet.Cells[2, index].Value.ToString().Trim().Replace(" (*)", ""))
                                {
                                    case "Mã khách hàng":
                                        if (customers.Exists(x => (x.CustomerCode == item.ToString().Trim())) == true)
                                        {
                                            customer.ImportError.Add(new ImportError(false, "Mã khách hàng đã trùng với khách hàng khác trong tệp nhập khẩu."));
                                        }
                                        if (_customerRepository.CheckDuplicate(null, item.ToString().Trim()) == true)
                                        {
                                            customer.ImportError.Add(new ImportError(false, "Mã khách hàng đã trùng với khách hàng khác trong hệ thống."));
                                        }
                                        customer.CustomerCode = item.ToString().Trim();
                                        break;
                                    case "Tên khách hàng":
                                        customer.FullName = item.ToString().Trim();
                                        break;
                                    case "Mã thẻ thành viên":
                                        customer.MemberCardCode = item.ToString().Trim();
                                        break;
                                    case "Nhóm khách hàng":
                                        customer.CustomerGroupName = item.ToString().Trim();
                                        break;
                                    case "Số điện thoại":
                                        if (customers.Exists(x => (x.PhoneNumber == item.ToString().Trim())) == true)
                                        {
                                            customer.ImportError.Add(new ImportError(false, "SĐT trùng với SĐT khách hàng khác trong tệp nhập khẩu."));
                                        }
                                        if (_customerRepository.CheckDuplicate(null, item.ToString().Trim()) == true)
                                        {
                                            customer.ImportError.Add(new ImportError(false, "SĐT đã tồn tại trong hệ thống."));
                                        }
                                        customer.PhoneNumber = item.ToString().Trim();
                                        break;
                                    case "Ngày sinh":
                                        customer.DateOfBirth = DateTime.Parse(formatDate(item.ToString().Trim()));
                                        break;
                                    case "Tên công ty":
                                        customer.CompanyName = item.ToString().Trim();
                                        break;
                                    case "Mã số thuế":
                                        customer.CompanyTaxCode = item.ToString().Trim();
                                        break;
                                    case "Email":
                                        customer.Email = item.ToString().Trim();
                                        break;
                                    case "Địa chỉ":
                                        customer.Address = item.ToString().Trim();
                                        break;
                                    case "Chú ý":
                                        customer.Attention = item.ToString().Trim();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                switch ((worksheet.Cells[2, index].Value.ToString().Trim().Replace("(*)", "")))
                                {
                                    case "Mã khách hàng":
                                        customer.ImportError.Add(new ImportError(false, "Mã khách hàng không được để trống"));
                                        break;
                                    case "Tên khách hàng":
                                        customer.ImportError.Add(new ImportError(false, "Tên khách hàng không được để trống"));
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        if (customer.ImportError.Count == 0)
                        {
                            customer.ImportError.Add(new ImportError(true, "Hợp lệ"));
                        }
                        customers.Add(customer);
                    }
                }
                _serviceResult.Data = customers;
            }
            return _serviceResult;

        }

        /// <summary>
        /// Format ngày tháng
        /// </summary>
        /// <param name="datetime">Ngày tháng</param>
        /// <returns>Ngày tháng đã được format</returns>
        /// Created by: duylv - 23/08/2021
        private string formatDate(string datetime)
        {
            var listStr = datetime.Split("/");
            var formatDatetime = string.Empty;
            if (listStr.Count() == 1)
            {
                formatDatetime = $"01/01/{datetime}";
            }
            else if (listStr.Count() == 2)
            {
                formatDatetime = $"01/{datetime}";
            }
            else
            {
                formatDatetime = datetime;
            }
            return formatDatetime;
        }

        /// <summary>
        /// Thêm danh sách khách hàng vào data base
        /// </summary>
        /// <param name="customers">Danh sách khách hàng</param>
        /// <returns>Số bản ghi được thêm vào</returns>
        /// Created by: duylv - 22/08/2021
        public int ImportData(List<Customer> customers)
        {
            var listValid = new List<Customer>();
            var valid = true;
            foreach (var item in customers)
            {

                if (item.ImportError.Exists(x => (x.Success)))
                {
                    valid = ValidateData(item);
                    valid = ValidateCustom(item);
                    if (!valid)
                        listValid.Add(item);
                }
            }

            var res = 0;
            if (listValid.Count > 0)
            {
                res = _customerRepository.InsertListCustomer(listValid);
            }
            return res;
        }

        /// <summary>
        /// Sư lí validate riêng
        /// </summary>
        /// <param name="customer">Thông tin customer</param>
        /// <returns>true là không lỗi, false là lỗi</returns>
        /// Created by: duylv - 16/08/2021
        protected override bool ValidateCustom(Customer customer)
        {

            // Kiểm tra trùng mã
            if (customer.CustomerCode != null)
            {
                var duplicate = _customerRepository.CheckDuplicate(customer.CustomerId, customer.CustomerCode);
                if (_serviceResult.IsValid == true && duplicate == true)
                {
                    var data = new
                    {
                        PropertyName = Resources.Resources.Name_CustomerCode,
                        ErrorInfo = Resources.Resources.Duplicate_CustomerCode,
                    };
                    _serviceResult.Data = data;
                    _serviceResult.Messenger = Resources.Resources.Duplicate_CustomerCode;
                    _serviceResult.IsValid = false;
                }
            }

            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            if (customer.Email != null)
            {
                bool testEmail = Regex.IsMatch(customer.Email, regex, RegexOptions.IgnoreCase);
                if (_serviceResult.IsValid == true && !testEmail)
                {
                    var data = new
                    {
                        PropertyName = Resources.Resources.Name_CustomerCode,
                        ErrorInfo = Resources.Resources.Error_Email,
                    };
                    _serviceResult.Data = data;
                    _serviceResult.Messenger = Resources.Resources.Duplicate_CustomerCode;
                    _serviceResult.IsValid = false;
                }
            }
            
            return _serviceResult.IsValid;
        }
        #endregion

    }
}
