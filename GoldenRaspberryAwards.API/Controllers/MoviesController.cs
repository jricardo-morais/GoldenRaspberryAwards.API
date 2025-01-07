using CsvHelper;
using GoldenRaspberryAwards.API.Application.Movies;
using GoldenRaspberryAwards.API.Domain.Entities;
using GoldenRaspberryAwards.API.Mappings;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace GoldenRaspberryAwards.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _service;

    public MoviesController(IMovieService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var movies = await _service.GetAll();
        return Ok(movies);
    }

    [HttpPost]
    public IActionResult UploadCsv(IFormFile file)
    {
        var movies = new List<Movie>();
        var configuration = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HeaderValidated = null, 
            MissingFieldFound = null
        };

        using (var reader = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvReader(reader, configuration))
        {
            csv.Context.RegisterClassMap<MovieMap>();
            movies = csv.GetRecords<Movie>().ToList();
            foreach (var movie in movies)
                _service.Insert(movie);
        }

        return Ok(movies);
    }
}
