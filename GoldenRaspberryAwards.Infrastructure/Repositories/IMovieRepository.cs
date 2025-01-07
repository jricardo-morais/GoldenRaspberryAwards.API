using GoldenRaspberryAwards.Domain.Entities;

namespace GoldenRaspberryAwards.Infrastructure.Repositories;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAll();
    Task<Movie> GetById(Guid id);
    Task<Movie> Insert(Movie movie);
    Task<Movie> Update(Movie movie);
    Task Delete(Movie movie);
    Task DeleteAll(List<Movie> movies);
}
