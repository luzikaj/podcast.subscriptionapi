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
        if (await _subService.ExistsAsync(""))
            return Conflict("Subscription already active");
        
        if (await _subService.AddAsync(""))
            return Ok("Subscription started");

        return BadRequest();
    }

    [HttpPost("stop")]
    public async Task<IActionResult> Stop()
    {
        if (!await _subService.ExistsAsync(""))
            return NotFound("No subscription active");
        
        if(await _subService.RemoveAsync(""))
            return Ok("Subscription ended");

        return BadRequest();
    }
}