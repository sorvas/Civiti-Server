using Civiti.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Civiti.Api.Data.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.TargetType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.Details)
            .HasMaxLength(500);

        // Unique index: one report per user per target
        builder.HasIndex(r => new { r.ReporterId, r.TargetType, r.TargetId })
            .IsUnique();

        // Index for looking up reports by target
        builder.HasIndex(r => new { r.TargetType, r.TargetId });

        // Relationships
        builder.HasOne(r => r.Reporter)
            .WithMany()
            .HasForeignKey(r => r.ReporterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
