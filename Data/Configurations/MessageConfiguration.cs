using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(m => m.Text).IsRequired();
            builder.Property(m => m.Date).IsRequired();
            builder.Property(m => m.ChatId).IsRequired();
            builder.Property(m => m.UserId).IsRequired();
        }
    }
}
