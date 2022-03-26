using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class QuettaDbContext : IdentityDbContext<User>
    {
        public QuettaDbContext(DbContextOptions<QuettaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .Ignore(u => u.AccessFailedCount)
                .Ignore(u => u.LockoutEnabled)
                .Ignore(u => u.LockoutEnd)
                .Ignore(u => u.PhoneNumber)
                .Ignore(u => u.PhoneNumberConfirmed)
                .Ignore(u => u.EmailConfirmed)
                .Ignore(u => u.Email)
                .Ignore(u => u.NormalizedEmail)
                .Ignore(u => u.TwoFactorEnabled)
                .Ignore(u => u.PasswordHash);
        }
    }
}
