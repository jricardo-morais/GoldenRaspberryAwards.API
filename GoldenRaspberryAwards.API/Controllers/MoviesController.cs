using GoldenRaspberryAwards.Application.Movies;
using GoldenRaspberryAwards.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberryAwards.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService service)
    {
        _movieService = service;
    }

   
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Movie>), 200)]
    [ProducesResponseType(typeof(IEnumerable<Movie>), 500)]
    public async Task<IActionResult> Get()
    {
        var movies = await _movieService.GetAll();
        return Ok(movies);
    }

   
    [HttpPost("Upload")]
    [ProducesResponseType(typeof(object), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> UploadMovieFile([FromForm] FormFileUpload formFile)
    {
        try
        {
            var result = await _movieService.UploadMovieFile(formFile.File);
            return Ok(new { Message = "Upload realizado com sucesso.", MoviesCount = result });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao processar arquivo: {ex.Message}");
        }
    }
}
