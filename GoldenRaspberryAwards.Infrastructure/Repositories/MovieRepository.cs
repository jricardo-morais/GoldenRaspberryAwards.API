using GoldenRaspberryAwards.Domain.Entities;
using GoldenRaspberryAwards.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberryAwards.Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _context;

    public MovieRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movie>> GetAll() => await GetAllMovies();

    public async Task<Movie> GetById(Guid id)
    {
        var producers = await GetAllMovies();
        return producers.Where(producer => producer.Id.Equals(id)).FirstOrDefault()!;
    }

    public async Task<IEnumerable<Movie>> GetAllMovies()
    {
        return await _context.Movies.ToListAsync();
    }

    public async Task<Movie> Insert(Movie movie)
    {
        movie.Id = Guid.NewGuid();
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<Movie> Update(Movie movie)
    {
        _context.Attach(movie);
        _context.Entry(movie).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task Delete(Movie movie)
    {
        _context.Remove(movie);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAll(List<Movie> movies)
    {
        _context.RemoveRange(movies);
        await _context.SaveChangesAsync();
    }
}
