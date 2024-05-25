using Microsoft.EntityFrameworkCore;
using WebAPI.Infrastructure.Entities;

namespace WebAPI.Infrastructure;

public class LoggingDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Log> Logs { get; set; }
}