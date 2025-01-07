using GoldenRaspberryAwards.API.Domain.Entities;

namespace GoldenRaspberryAwards.API.Application.Movies;

public interface IMovieService
{
    Task<IEnumerable<Movie>> GetAll();
    Task<Movie> GetById(Guid id);
    Task<Movie> Insert(Movie movie);
    Task<Movie> Update(Movie movie);
    Task Delete(Guid id);
}
