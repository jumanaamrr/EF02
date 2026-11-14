using EF02.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF02.Configurations;

/// <summary>
/// Fluent API configuration for <see cref="Badge"/>.
/// Uses the separate IEntityTypeConfiguration&lt;T&gt; approach.
/// </summary>
public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
{
    public void Configure(EntityTypeBuilder<Badge> builder)
    {
        // ── Table ────────────────────────────────────────────────────────────
        builder.ToTable("Badges");

        // ── Primary key ──────────────────────────────────────────────────────
        builder.HasKey(b => b.Id);

        // ── Properties ───────────────────────────────────────────────────────
        builder.Property(b => b.BadgeNumber)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(b => b.IssuedDate)
               .IsRequired();

        // Store the enum as its string name for readability in the database.
        builder.Property(b => b.Tier)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20);

        // ── Unique constraint: one badge per attendee ─────────────────────────
        builder.HasIndex(b => b.AttendeeId)
               .IsUnique();

        // ── One-to-One: Attendee ←→ Badge ────────────────────────────────────
        // Badge is the DEPENDENT end; AttendeeId is the FK.
        builder.HasOne(b => b.Attendee)
               .WithOne(a => a.Badge)
               .HasForeignKey<Badge>(b => b.AttendeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
