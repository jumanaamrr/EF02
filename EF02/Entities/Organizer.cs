using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF02.Entities;

/// <summary>
/// Represents a person or company that hosts events on the platform.
/// Configured entirely with Data Annotations.
/// </summary>
[Table("EventOrganizers")]
public class Organizer
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    // Optional — no [Required]
    [MaxLength(150)]
    public string? CompanyName { get; set; }

    public bool IsVerified { get; set; }

    // ── Navigation properties ────────────────────────────────────────────────

    /// <summary>Dependent end of the 1-to-1 relationship with OrganizerProfile.</summary>
    public OrganizerProfile? Profile { get; set; }

    /// <summary>Collection end of the 1-to-Many relationship with Event.</summary>
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
