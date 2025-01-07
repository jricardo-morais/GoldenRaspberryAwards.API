using GoldenRaspberryAwards.Application.Awards;
using GoldenRaspberryAwards.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberryAwards.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AwardsIntervalsController : ControllerBase
{
    private readonly IAwardService _awardService;

    public AwardsIntervalsController(IAwardService service)
    {
        _awardService = service;
    }

   
    [HttpGet("Intervals")]
    [ProducesResponseType(typeof(IEnumerable<Movie>), 200)]
    [ProducesResponseType(typeof(IEnumerable<Movie>), 500)]
    public async Task<IActionResult> GetAwardIntervals()
    {
        var intervals = await _awardService.GetAwardIntervals();
        return Ok(intervals);
    }
}
