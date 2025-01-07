using GoldenRaspberryAwards.API.Data.Repositories;
using GoldenRaspberryAwards.API.Domain.Entities;

namespace GoldenRaspberryAwards.API.Application.Movies;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _repository;

    public MovieService(IMovieRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Movie>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Movie> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task<Movie> Insert(Movie movie)
    {
        return await _repository.Insert(movie);
    }

    public async Task<Movie> Update(Movie movie)
    {
        return await _repository.Update(movie);
    }

    public async Task Delete(Guid id)
    {
        var producer = await _repository.GetById(id);
        await _repository.Delete(producer);
    }
}
