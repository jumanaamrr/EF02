namespace EF02.Entities;

/// <summary>Credential tier assigned to a badge.</summary>
public enum BadgeTier
{
    Standard,
    VIP
}

/// <summary>
/// A credential issued to an attendee.
/// Configured entirely with Fluent API (via a separate IEntityTypeConfiguration class).
/// </summary>
public class Badge
{
    public int Id { get; set; }

    /// <summary>Unique badge number per attendee.</summary>
    public string BadgeNumber { get; set; } = string.Empty;

    public DateTime IssuedDate { get; set; }

    public BadgeTier Tier { get; set; }

    // ── Foreign key + navigation ─────────────────────────────────────────────

    public int AttendeeId { get; set; }

    public Attendee Attendee { get; set; } = null!;
}
