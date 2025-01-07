using CsvHelper;
using CsvHelper.Configuration;
using GoldenRaspberryAwards.API.Data.Contexts;
using GoldenRaspberryAwards.API.Domain.Entities;
using GoldenRaspberryAwards.API.Mappings;
using System.Globalization;

namespace GoldenRaspberryAwards.API.Application.DataSeed
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
                Delimiter = ";", // Define o delimitador como ponto e vírgula
                HeaderValidated = null, // Ignora validação de cabeçalhos ausentes
                MissingFieldFound = null // Ignora validação de campos ausentes
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, csvConfig);

            // Registra o mapa para Movie
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

        //public class MovieCsvRecord
        //{
        //    public Guid Id { get; set; }
        //    public string? Title { get; set; }
        //    public int Year { get; set; }
        //    public string? Studios { get; set; }
        //    public string? Producers { get; set; }
        //    public bool Winner { get; set; }
        //}

        //public class MovieCsvRecordMap : ClassMap<MovieCsvRecord>
        //{
        //    public MovieCsvRecordMap()
        //    {
        //        Map(m => m.Id).Ignore();
        //        Map(m => m.Year).Name("year");
        //        Map(m => m.Title).Name("title");
        //        Map(m => m.Studios).Name("studios");
        //        Map(m => m.Producers).Name("producers");
        //        Map(m => m.Winner).Name("winner");
        //    }
        //}
    }
}
