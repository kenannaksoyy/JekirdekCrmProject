using JekirdekCrm.CrossCutting.Exceptions;
using JekirdekCrm.Domain.Dto.Request;
using JekirdekCrm.Domain.Dto.Response;
using JekirdekCrm.Domain.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JekirdekCrm.Api.Controllers
{
    /// <summary>
    /// CustomerController Admin Ve User�n Uygulayaca�� M��terilere Y�nelik Crud Servisleri Bulunmaktad�r
    /// M��terilerin Get Endpointleri Admin ve User Eri�ebilir
    /// Create Update ve Delete Endpointlerine Sadece Admin Eri�ebilir
    /// Authorization Authorize Attribute ile Rol Bazl� Sa�lanmaktad�r
    /// �e�itli Validasyonlara G�re Uygun Statulerde D�n�� Sa�lamaktad�r
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private const string CUSTOMER_UNEXPEXTED_ERROR = "M��teri Servisinde Beklenmedik Hata Olu�tu: ";
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        /// <summary>
        /// M��terileri Getirmektedir
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCustomers")]
        //Admin Ve User Eri�ebilir
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
        /// Queryden Id Al�p M��teri Bilgilerini Getirir
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCustomer/{id}")]
        //Admin Ve User Eri�ebilir
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
            //Servisden Y�netilemi� Gelen Hatalar� Handle Et Uygun Statude D�n Handle Edilmediyse 500 G�nder
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
        /// Bodyden M��teri Bilgilerini Al�p Yeni M��teri Olu�turur
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        [HttpPut("CreateCustomer")]
        //Sadece Admin Eri�ebilir
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
        /// Bodyden M��teri Bilgilerini Al�r M��teriyi G�nceller
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        [HttpPost("UpdateCustomer")]
        //Sadece Admin Eri�ebilir
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
                    //�e�itli Hatalara G�re Uygun Statu D�n���
                    NotFoundException => NotFound(new { IsError = true, ErrorMessage = ex.Message }),
                    ConflictException => StatusCode(StatusCodes.Status409Conflict, new { IsError = true, ErrorMessage = ex.Message }),
                    MissingFieldException => BadRequest(new { IsError = true, ErrorMessage = ex.Message }),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, new { IsError = true, ErrorMessage = CUSTOMER_UNEXPEXTED_ERROR + ex.Message })

                };
            }
        }

        /// <summary>
        /// Queryden ald��� Id Bilgisi �le M��teri Silmektedir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteCustomer/{id}")]
        //Sadece Admin Eri�ebilir
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
