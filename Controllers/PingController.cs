using Microsoft.AspNetCore.Mvc;

namespace MyApiProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            message = "Simple API is running.",
            timestamp = DateTime.UtcNow
        });
    }
}
