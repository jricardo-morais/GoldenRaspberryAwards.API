using Microsoft.EntityFrameworkCore;
using GoldenRaspberryAwards.Domain.Entities;

namespace GoldenRaspberryAwards.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Movie> Movies { get; set; }

}
