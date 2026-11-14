using EF02.Configurations;
using EF02.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF02.Data;

/// <summary>
/// EF Core DbContext for the EventHub platform.
/// </summary>
public class EventHubDbContext : DbContext
{
    // ── DbSets ───────────────────────────────────────────────────────────────

    public DbSet<Organizer> Organizers => Set<Organizer>();
    public DbSet<OrganizerProfile> OrganizerProfiles => Set<OrganizerProfile>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Attendee> Attendees => Set<Attendee>();
    public DbSet<Badge> Badges => Set<Badge>();

    // ── Construction ─────────────────────────────────────────────────────────

    public EventHubDbContext(DbContextOptions<EventHubDbContext> options)
        : base(options) { }

    // ── Model configuration ───────────────────────────────────────────────────

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ── WAY 1: separate IEntityTypeConfiguration classes ─────────────────
        // OrganizerProfile and Badge are fully configured in their own classes.
        modelBuilder.ApplyConfiguration(new OrganizerProfileConfiguration());
        modelBuilder.ApplyConfiguration(new BadgeConfiguration());

        // ── WAY 2: inline Fluent API configuration ───────────────────────────
        // Event is configured directly here in OnModelCreating.
        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Events");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.Description)
                  .IsRequired();

            entity.Property(e => e.StartDate)
                  .IsRequired();

            // EndDate is optional — no IsRequired()
            entity.Property(e => e.EndDate);

            entity.Property(e => e.MaxAttendees)
                  .IsRequired();

            // ── One-to-Many: Organizer (1) ←→ Event (Many) ──────────────────
            // Event is the DEPENDENT end; OrganizerId is the FK.
            entity.HasOne(e => e.Organizer)
                  .WithMany(o => o.Events)
                  .HasForeignKey(e => e.OrganizerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
