
using BookingSystem.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BookingSystem.Cli;


public class BookingContextFactory : IDesignTimeDbContextFactory<BookingDbContext>
{
    public BookingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BookingDbContext>();
        var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
        var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new Exception("Missing Connection String");
        optionsBuilder.UseSqlServer(connectionString);
        return new BookingDbContext(optionsBuilder.Options);
    }
}
