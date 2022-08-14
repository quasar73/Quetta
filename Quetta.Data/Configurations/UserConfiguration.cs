using Quetta.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Quetta.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
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

            builder
                .HasMany(u => u.InvitesIncoming)
                .WithOne(i => i.Receiver);

            builder
                .HasMany(u => u.InvitesOutcoming)
                .WithOne(i => i.Sender);

            builder
                .HasMany(u => u.Chats)
                .WithMany(c => c.Users);

            builder
                .HasMany(u => u.CreatedChats)
                .WithOne(c => c.Creator);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
