using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GwpApi.Controllers
{
    [Route("api/gwp")]
    public class CountryGwpController : ControllerBase
    {
        private readonly IGwpService _gwpService;

        public CountryGwpController(IGwpService gwpService)
        {
            _gwpService = gwpService;
        }

        [HttpPost("avg")]
        public async Task<IActionResult> CalculateAverage([FromBody] GwpRequestModel request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Invalid input");
                }

                var result = await _gwpService.GetAverageGwp(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
