using Microsoft.AspNetCore.Mvc;
using Podcast.SubscriptionApi.Services;

namespace Podcast.SubscriptionApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subService;
    
    public SubscriptionController(ISubscriptionService subService)
    {
        _subService = subService;
    }

    [HttpPost("start")]
    public async Task<IActionResult> Start()
    {
        if (!HttpContext.Request.Headers.ContainsKey("X-Custom-Auth"))
            return Unauthorized();

        var email = HttpContext.Request.Headers["X-Custom-Auth"];
        if (string.IsNullOrEmpty(email))
            return Unauthorized();
        
        if (await _subService.ExistsAsync(email))
            return Conflict("Subscription already active");
        
        if (await _subService.AddAsync(email))
            return Ok("Subscription started");

        return NotFound();
    }

    [HttpPost("stop")]
    public async Task<IActionResult> Stop()
    {
        if (!HttpContext.Request.Headers.ContainsKey("X-Custom-Auth"))
            return Unauthorized();

        var email = HttpContext.Request.Headers["X-Custom-Auth"];
        if (string.IsNullOrEmpty(email))
            return Unauthorized();
        
        if (!await _subService.ExistsAsync(email))
            return NotFound("No subscription active");
        
        if(await _subService.RemoveAsync(email))
            return Ok("Subscription ended");

        return BadRequest();
    }
}