using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class InviteConfiguration : IEntityTypeConfiguration<Invite>
    {
        public void Configure(EntityTypeBuilder<Invite> builder)
        {
            builder.Property(i => i.IsGroupChat).IsRequired();
            builder.Property(i => i.SenderId).IsRequired();
            builder.Property(i => i.ReceiverId).IsRequired();
            builder.Property(i => i.Status).IsRequired();
        }
    }
}
