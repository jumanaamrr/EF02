using EF02.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF02.Configurations;

/// <summary>
/// Fluent API configuration for <see cref="OrganizerProfile"/>.
/// Uses the separate IEntityTypeConfiguration&lt;T&gt; approach.
/// </summary>
public class OrganizerProfileConfiguration : IEntityTypeConfiguration<OrganizerProfile>
{
    public void Configure(EntityTypeBuilder<OrganizerProfile> builder)
    {
        // ── Table ────────────────────────────────────────────────────────────
        builder.ToTable("OrganizerProfiles");

        // ── Primary key ──────────────────────────────────────────────────────
        builder.HasKey(p => p.Id);

        // ── Properties ───────────────────────────────────────────────────────
        builder.Property(p => p.Bio)
               .HasMaxLength(1000);

        builder.Property(p => p.WebsiteUrl)
               .HasMaxLength(500);

        builder.Property(p => p.LogoUrl)
               .HasMaxLength(500);

        // ── One-to-One: Organizer ←→ OrganizerProfile ────────────────────────
        // OrganizerProfile is the DEPENDENT end.
        // HasForeignKey uses OrganizerId so the FK column is explicit.
        builder.HasOne(p => p.Organizer)
               .WithOne(o => o.Profile)
               .HasForeignKey<OrganizerProfile>(p => p.OrganizerId)
               .OnDelete(DeleteBehavior.Cascade); // profile is deleted when organizer is deleted
    }
}
