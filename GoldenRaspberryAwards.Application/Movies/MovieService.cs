
using CsvHelper;
using GoldenRaspberryAwards.Application.Movies;
using GoldenRaspberryAwards.Domain.Entities;
using GoldenRaspberryAwards.Domain.Mappings;
using GoldenRaspberryAwards.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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

    public async Task<int> UploadMovieFile(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0)
            throw new ArgumentException("O arquivo é obrigatório.");

        var configuration = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HeaderValidated = null,
            MissingFieldFound = null
        };

        var movies = new List<Movie>();
        using (var reader = new StreamReader(formFile.OpenReadStream()))
        using (var csv = new CsvReader(reader, configuration))
        {
            csv.Context.RegisterClassMap<MovieMap>();
            movies = csv.GetRecords<Movie>().ToList();

            if(movies != null)
            {
                await DeleteAllMovies();

                foreach (var movie in movies)
                    await _repository.Insert(movie);
            }
               
        }

        return movies.Count;

    }

    public async Task DeleteAllMovies()
    {
        var producers = await _repository.GetAll();
        if(producers.Any())
            await _repository.DeleteAll(producers.ToList());
    }
}
