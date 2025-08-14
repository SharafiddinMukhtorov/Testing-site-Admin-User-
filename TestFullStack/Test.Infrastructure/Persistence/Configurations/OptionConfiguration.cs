using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Domain.Entities;

namespace Test.Infrastructure.Persistence.Configurations;

public class OptionConfiguration : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Text)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(o => o.IsCorrect)
               .IsRequired();

        builder.HasOne(o => o.Question)
               .WithMany(q => q.Options)
               .HasForeignKey(o => o.QuestionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
