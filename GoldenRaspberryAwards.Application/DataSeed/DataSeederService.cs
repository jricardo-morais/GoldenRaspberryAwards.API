using CsvHelper;
using CsvHelper.Configuration;
using GoldenRaspberryAwards.Domain.Entities;
using GoldenRaspberryAwards.Domain.Mappings;
using GoldenRaspberryAwards.Infrastructure.Contexts;
using System.Globalization;
using System.IO;

namespace GoldenRaspberryAwards.Application.DataSeed
{
    public class DataSeederService
    {
        private readonly AppDbContext _context;

        public DataSeederService(AppDbContext context)
        {
            _context = context;
        }

        public void Seed(string filePath)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";", 
                HeaderValidated = null, 
                MissingFieldFound = null 
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, csvConfig);

            csv.Context.RegisterClassMap<MovieMap>();
            var movieRecords = csv.GetRecords<Movie>();

            foreach (var movie in movieRecords)
            {

                Console.WriteLine($"Title: {movie.Title}, Year: {movie.Year}, Producers: {movie.Producers}, Winner: {movie.Winner}");

                var winners = _context.Movies.Where(m => m.Winner).ToList();
                Console.WriteLine($"Total Winners: {winners.Count}");

                _context.Movies.Add(movie);
                _context.SaveChanges();

            }

        }
    }
}
