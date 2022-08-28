using Microsoft.EntityFrameworkCore;
using Npgsql;
using Quetta.Data;

namespace Quetta.Tests.Handlers
{
    public class TestQuettaDbContextFactory
    {
        private readonly DbContextOptions<QuettaDbContext> options;

        public TestQuettaDbContextFactory()
        {
            options = new DbContextOptionsBuilder<QuettaDbContext>()
                .UseInMemoryDatabase(databaseName: "QuettaDb")
                .Options;
        }

        public QuettaDbContext Create() => new QuettaDbContext(options);
    }
}
