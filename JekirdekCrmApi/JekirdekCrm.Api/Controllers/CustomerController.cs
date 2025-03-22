using JekirdekCrm.Domain.Dto.Response;
using JekirdekCrm.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace JekirdekCrm.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        
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
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
