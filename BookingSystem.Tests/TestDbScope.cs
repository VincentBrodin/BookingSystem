using BookingSystem.Core.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Tests;

public class TestDbScope : IDisposable
{
    public SqliteConnection Connection { get; }
    public BookingDbContext Context { get; }

    public TestDbScope()
    {
        Connection = new SqliteConnection("DataSource=:memory:");
        Connection.Open();

        var options = new DbContextOptionsBuilder<BookingDbContext>()
            .UseSqlite(Connection)
            .Options;

        Context = new BookingDbContext(options);
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context.Dispose();
        Connection.Dispose();
    }
}
