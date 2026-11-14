namespace EF02.Entities;

/// <summary>
/// Public-facing profile page for an organizer.
/// Configured entirely with Fluent API (via a separate IEntityTypeConfiguration class).
/// </summary>
public class OrganizerProfile
{
    public int Id { get; set; }

    public string? Bio { get; set; }

    public string? WebsiteUrl { get; set; }

    public string? LogoUrl { get; set; }

    // ── Foreign key + navigation (principal end navigates here) ─────────────

    /// <summary>
    /// FK that is also the PK — "shared primary key" pattern enforces the
    /// 1-to-1 relationship and ensures the profile cannot exist without
    /// its owning organizer.
    /// </summary>
    public int OrganizerId { get; set; }

    public Organizer Organizer { get; set; } = null!;
}
