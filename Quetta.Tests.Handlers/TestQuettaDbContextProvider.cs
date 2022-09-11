using Microsoft.EntityFrameworkCore;
using Npgsql;
using Quetta.Data;

namespace Quetta.Tests.Handlers
{
    public class TestQuettaDbContextProvider : IDisposable
    {
        public QuettaDbContext DbContext { get; set; }

        public TestQuettaDbContextProvider()
        {
            var options = new DbContextOptionsBuilder<QuettaDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            DbContext = new QuettaDbContext(options);
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
        }
    }
}
