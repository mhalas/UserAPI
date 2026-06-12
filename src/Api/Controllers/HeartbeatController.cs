using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Controller for checking the health status of the API.
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    public class HeartbeatController : ControllerBase
    {
        /// <summary>
        /// Returns the health status of the API.
        /// </summary>
        /// <returns>True if the API is healthy.</returns>
        [HttpGet]
        public IActionResult HeartbeatStatus()
        {
            return Ok(true);
        }
    }
}
