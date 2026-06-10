using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HeartbeatController : ControllerBase
    {
        [HttpGet]
        public IActionResult HeartbeatStatus()
        {
            return Ok(true);
        }
    }
}
