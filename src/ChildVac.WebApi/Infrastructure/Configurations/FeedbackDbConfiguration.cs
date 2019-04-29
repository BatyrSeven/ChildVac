using ChildVac.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChildVac.WebApi.Infrastructure.Configurations
{
    public class FeedbackDbConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(10000);

            builder.Property(x => x.DateTime)
                .IsRequired();
        }
    }
}