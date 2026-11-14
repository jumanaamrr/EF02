namespace EF02.Entities;

/// <summary>
/// An event published by an organizer on the platform.
/// Configured entirely with Fluent API (inline in OnModelCreating).
/// </summary>
public class Event
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int MaxAttendees { get; set; }

    // ── Foreign key + navigation ─────────────────────────────────────────────

    public int OrganizerId { get; set; }

    public Organizer Organizer { get; set; } = null!;
}
