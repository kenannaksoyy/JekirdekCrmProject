using JekirdekCrm.CrossCutting.Exceptions;
using JekirdekCrm.Domain.Dto.Request;
using JekirdekCrm.Domain.Dto.Response;
using JekirdekCrm.Domain.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JekirdekCrm.Api.Controllers
{
    /// <summary>
    /// CustomerController Admin Ve Userýn Uygulayacaðý Müþterilere Yönelik Crud Servisleri Bulunmaktadýr
    /// Müþterilerin Get Endpointleri Admin ve User Eriþebilir
    /// Create Update ve Delete Endpointlerine Sadece Admin Eriþebilir
    /// Authorization Authorize Attribute ile Rol Bazlý Saðlanmaktadýr
    /// Çeþitli Validasyonlara Göre Uygun Statulerde Dönüþ Saðlamaktadýr
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private const string CUSTOMER_UNEXPEXTED_ERROR = "Müþteri Servisinde Beklenmedik Hata Oluþtu: ";
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        /// <summary>
        /// Müþterileri Getirmektedir
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCustomers")]
        //Admin Ve User Eriþebilir
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetCustomersAsync()
        {
            try
            {
                List<CustomerResponse> customerResponses = await _customerService.GetAllAsync();
                return Ok(new
                {
                    IsError = false,
                    Customers = customerResponses
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    IsError = true,
                    ErrorMessage = CUSTOMER_UNEXPEXTED_ERROR + ex.Message
                });
            }
        }

        /// <summary>
        /// Queryden Id Alýp Müþteri Bilgilerini Getirir
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCustomer/{id}")]
        //Admin Ve User Eriþebilir
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            try
            {
                CustomerResponse customerResponse = await _customerService.GetByIdAsync(id);
                return Ok(new
                {
                    IsError = false,
                    Customer = customerResponse
                });
            }
            //Servisden Yönetilemiþ Gelen Hatalarý Handle Et Uygun Statude Dön Handle Edilmediyse 500 Gönder
            catch (Exception ex)
            {
                return ex switch
                {
                    NotFoundException => NotFound(new { IsError = true, ErrorMessage = ex.Message }),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, new { IsError = true, ErrorMessage = CUSTOMER_UNEXPEXTED_ERROR + ex.Message })
                };
            }
        }

        /// <summary>
        /// Bodyden Müþteri Bilgilerini Alýp Yeni Müþteri Oluþturur
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        [HttpPut("CreateCustomer")]
        //Sadece Admin Eriþebilir
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCustomerAsync(CustomerRequest customerRequest)
        {
            try
            {
                int createCustomerId = await _customerService.CreateAsync(customerRequest);
                return StatusCode(StatusCodes.Status201Created, new
                {
                    IsError = false,
                    NewCustomerId = createCustomerId
                });
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    ConflictException => StatusCode(StatusCodes.Status409Conflict,new { IsError = true, ErrorMessage = ex.Message }),
                    MissingFieldException => BadRequest(new { IsError = true, ErrorMessage = ex.Message }),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, new { IsError = true, ErrorMessage = CUSTOMER_UNEXPEXTED_ERROR + ex.Message })
                };

            }
        }

        /// <summary>
        /// Bodyden Müþteri Bilgilerini Alýr Müþteriyi Günceller
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        [HttpPost("UpdateCustomer")]
        //Sadece Admin Eriþebilir
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCustomerAsync(CustomerRequest customerRequest)
        {
            try
            {
                await _customerService.UpdateAsync(customerRequest);
                return NoContent();
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    //Çeþitli Hatalara Göre Uygun Statu Dönüþü
                    NotFoundException => NotFound(new { IsError = true, ErrorMessage = ex.Message }),
                    ConflictException => StatusCode(StatusCodes.Status409Conflict, new { IsError = true, ErrorMessage = ex.Message }),
                    MissingFieldException => BadRequest(new { IsError = true, ErrorMessage = ex.Message }),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, new { IsError = true, ErrorMessage = CUSTOMER_UNEXPEXTED_ERROR + ex.Message })

                };
            }
        }

        /// <summary>
        /// Queryden aldýðý Id Bilgisi Ýle Müþteri Silmektedir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteCustomer/{id}")]
        //Sadece Admin Eriþebilir
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            try
            {
                await _customerService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    NotFoundException => NotFound(new { IsError = true, ErrorMessage = ex.Message }),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, new { IsError = true, ErrorMessage = CUSTOMER_UNEXPEXTED_ERROR + ex.Message })
                };
            }
        }
    }
}
