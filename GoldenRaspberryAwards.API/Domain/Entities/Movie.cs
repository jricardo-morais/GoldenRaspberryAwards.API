namespace GoldenRaspberryAwards.API.Domain.Entities;

public class Movie
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public int Year { get; set; }
    public string? Studios { get; set; }
    public string? Producers { get; set; }
    public bool Winner { get; set; }
}
