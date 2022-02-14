using Acrelec.SCO.Server.Dtos;
using Acrelec.SCO.Server.Model;
using Acrelec.SCO.Server.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Acrelec.SCO.Server.Controllers.v1
{
    [ApiController]
    public class ScoController : Controller
    {
        private readonly IOrderValidation _orderValidation;
        public ScoController(IOrderValidation orderValidation)
        {
            _orderValidation = orderValidation;
        }
        [HttpGet]
        [Route("api-sco/v1/availability")]
        public IActionResult Get()
        {
            return Ok(new CheckAvailabilityResponseDto
            {
                CanInjectOrders = true
            });
        }

        [HttpPost]
        [Route("api-sco/v1/injectorder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult InjectOrder(InjectOrderRequest injectOrderRequest)
        {
            var validationResult = _orderValidation.ValidateRequest(injectOrderRequest);
            if (validationResult?.Count == 0)
                return Ok(new InjectOrderResponseDto
                {
                    OrderNumber = "10"
                });

            return BadRequest(validationResult);
        }
    }
}
