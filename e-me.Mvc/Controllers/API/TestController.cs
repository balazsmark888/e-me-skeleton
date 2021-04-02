using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_me.Mvc.Controllers.API
{
    /// <summary>
    /// Test controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Test endpoint.
        /// </summary>
        /// <returns>Status code with the test "Testing"</returns>
        [AllowAnonymous]
        public IActionResult Test()
        {
            return Ok("Testing");
        }
    }
}
