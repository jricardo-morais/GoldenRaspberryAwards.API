using GoldenRaspberryAwards.API.Application.Awards;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberryAwards.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AwardIntervalsController : ControllerBase
{
    private readonly IAwardService _service;

    public AwardIntervalsController(IAwardService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAwardIntervals()
    {
        var intervals = await _service.GetAwardIntervals();
        return Ok(intervals);
    }
}
