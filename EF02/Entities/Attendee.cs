using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF02.Entities;

/// <summary>
/// Represents a person who attends events on the platform.
/// Configured entirely with Data Annotations.
/// </summary>
public class Attendee
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    // ── Home address fields ──────────────────────────────────────────────────

    [Required]
    [MaxLength(150)]
    public string Street { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string City { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Country { get; set; } = string.Empty;

    /// <summary>Stored in a column named ZipCode via the Column attribute.</summary>
    [Required]
    [MaxLength(20)]
    [Column("ZipCode")]
    public string PostalCode { get; set; } = string.Empty;

    // ── Computed / read-only property — must NOT be mapped ──────────────────

    /// <summary>
    /// Convenience read-only property that concatenates all address parts.
    /// The [NotMapped] attribute prevents EF Core from creating a column for it.
    /// </summary>
    [NotMapped]
    public string FullAddress => $"{Street}, {City}, {Country} {PostalCode}";

    // ── Navigation property ──────────────────────────────────────────────────

    /// <summary>Dependent end of the 1-to-1 relationship with Badge.</summary>
    public Badge? Badge { get; set; }
}
