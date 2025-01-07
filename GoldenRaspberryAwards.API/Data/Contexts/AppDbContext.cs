using Microsoft.EntityFrameworkCore;
using GoldenRaspberryAwards.API.Domain.Entities;

namespace GoldenRaspberryAwards.API.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Movie> Movies { get; set; }

}
