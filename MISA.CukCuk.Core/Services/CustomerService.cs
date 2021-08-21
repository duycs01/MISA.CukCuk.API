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
        ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public int DeleteListId(List<Guid> listId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Phân trang và lọc dữ liệu
        /// </summary>
        /// <param name="filterName">Lọc họ tên, mã khách hàng, số điện thoại</param>
        /// <param name="customerGroupId">Lọc theo nhóm khách hàng</param>
        /// <param name="pageSize">Số dữ liệu trên 1 trang</param>
        /// <param name="pageIndex"> Vị trí của trang </param>
        /// <returns></returns>
        /// Created by: duylv - 18/08/2021
        /// 
        public Paging GetCustomerPaging(string filterName, Guid? customerGroupId, int pageSize, int pageIndex)
        {
            return _customerRepository.GetCustomerPaging(filterName, customerGroupId, pageSize, pageIndex);
        }

        public ServiceResult Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null)
            {
                var data = new
                {
                    PropertyName = Resources.Resources.Error_ImportFile,
                    ErrorInfo = Resources.Resources.Error_ImportFile,

                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = data;
            }
            //if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            //{
            //    _serviceResult.Messenger = Resources.Resources.Error_ImportFile,;

            //    return _serviceResult;
            //}

            var customers = new List<Customer>();

            using (var stream = new MemoryStream())
            {
                formFile.CopyToAsync(stream, cancellationToken);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    for (int row = 3; row <= rowCount; row++)
                    {
                        var customer = new Customer();
                        for (int index = 1; index < 10; index++)
                        {
                            var item = worksheet.Cells[row, index].Value;
                            if (item != null)
                            {
                                switch (index)
                                {
                                    case 1:
                                        if (customers.Exists(x => (x.CustomerCode == item.ToString().Trim())) == true)
                                        {
                                            customer.ImportError.Add("Mã khách hàng đã trùng với khách hàng khác trong tệp nhập khẩu.");
                                        }
                                        if (_customerRepository.CheckDuplicate(null, item.ToString().Trim()) == true)
                                        {
                                            customer.ImportError.Add("Mã khách hàng đã trùng với khách hàng khác trong hệ thống.");
                                        }
                                        customer.CustomerCode = item.ToString().Trim();
                                        break;
                                    case 2:
                                        customer.FullName = item.ToString().Trim();
                                        break;
                                    case 3:
                                        customer.MemberCardCode = item.ToString().Trim();
                                        break;
                                    case 4:
                                        customer.CustomerGroupName = item.ToString().Trim();
                                        break;
                                    case 5:
                                        if (customers.Exists(x => (x.PhoneNumber == item.ToString().Trim())) == true)
                                        {
                                            var mess = "SĐT trùng với SĐT khách hàng khác trong tệp nhập khẩu.";
                                            customer.ImportError.Add( mess);
                                        }
                                        if (_customerRepository.CheckDuplicate(null, item.ToString().Trim()) == true)
                                        {
                                            customer.ImportError.Add( "SĐT đã tồn tại trong hệ thống.");
                                        }
                                        customer.PhoneNumber = item.ToString().Trim();
                                        break;  
                                    case 6:
                                        customer.DateOfBirth = DateTime.Parse(item.ToString().Trim());
                                        break;
                                    case 7:
                                        customer.CompanyName = item.ToString().Trim();
                                        break;
                                    case 8:
                                        customer.CompanyTaxCode = Int32.Parse(item.ToString().Trim());
                                        break;
                                    case 9:
                                        customer.Email = item.ToString().Trim();
                                        break;
                                    case 10:
                                        customer.Address = item.ToString().Trim();
                                        break;
                                    default:
                                        break;
                                }

                            }
                        }

                        customers.Add(customer);
                    }
                }
                _customerRepository.InsertListCustomer(customers);
                _serviceResult.Data = customers;
                return _serviceResult;
            }
        }


        /// <summary>
        /// Sư lí validate riêng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// Created by: duylv - 16/08/2021
        protected override bool ValidateCustom(Customer customer)
        {

            // Kiểm tra trùng mã
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

            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool testEmail = Regex.IsMatch(customer.Email, regex, RegexOptions.IgnoreCase);
            if (_serviceResult.IsValid == true && testEmail)
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
            return _serviceResult.IsValid;
        }
    }
}
