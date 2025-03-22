using JekirdekCrm.Domain.Interface.Services;
using JekirdekCrm.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace JekirdekCrm.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet("GetCustomers")]
        public async Task<IActionResult> GetCustomersAsync()
        {
            try
            {
                List<CustomerModel> customerModels = await _customerService.GetAllAsync();
                return Ok(customerModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
