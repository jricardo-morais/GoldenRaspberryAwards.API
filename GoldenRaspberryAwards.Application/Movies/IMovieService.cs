using GoldenRaspberryAwards.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace GoldenRaspberryAwards.Application.Movies;

public interface IMovieService
{
    Task<IEnumerable<Movie>> GetAll();
    Task<int> UploadMovieFile(IFormFile formFile);
    Task DeleteAllMovies();
}
