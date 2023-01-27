using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            DbContext = new QuettaDbContext(options);
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
        }
    }
}
