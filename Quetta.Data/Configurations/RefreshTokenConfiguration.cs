using Quetta.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Quetta.Data.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(rt => rt.Token).IsRequired();
            builder.Property(rt => rt.Expires).IsRequired();
            builder.Property(rt => rt.UserId).IsRequired();
        }
    }
}
