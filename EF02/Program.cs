using EF02.Data;
using EF02.Entities;
using Microsoft.EntityFrameworkCore;

// ── Configure the DbContext (LocalDB for quick local testing) ─────────────────
var options = new DbContextOptionsBuilder<EventHubDbContext>()
    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EventHubDb;Trusted_Connection=True;")
    .Options;

using var context = new EventHubDbContext(options);

// Create the database schema (creates tables if they don't exist).
context.Database.EnsureCreated();

Console.WriteLine("EventHub database schema created successfully.");

// ── Seed a quick demo ─────────────────────────────────────────────────────────

if (!context.Organizers.Any())
{
    var organizer = new Organizer
    {
        Name = "Alice Johnson",
        CompanyName = "TechConf Inc.",
        IsVerified = true,
        Profile = new OrganizerProfile
        {
            Bio = "Passionate about connecting developers worldwide.",
            WebsiteUrl = "https://techconf.example.com",
            LogoUrl = "https://techconf.example.com/logo.png"
        },
        Events =
        {
            new Event
            {
                Title = "Global Dev Summit 2026",
                Description = "A three-day summit for software engineers across the globe.",
                StartDate = new DateTime(2026, 9, 10),
                EndDate   = new DateTime(2026, 9, 12),
                MaxAttendees = 500
            }
        }
    };

    context.Organizers.Add(organizer);

    var attendee = new Attendee
    {
        FullName   = "Bob Smith",
        Email      = "bob.smith@example.com",
        Street     = "123 Main Street",
        City       = "Cairo",
        Country    = "Egypt",
        PostalCode = "11511",
        Badge = new Badge
        {
            BadgeNumber = "BADGE-0001",
            IssuedDate  = DateTime.UtcNow,
            Tier        = BadgeTier.VIP
        }
    };

    context.Attendees.Add(attendee);
    context.SaveChanges();

    Console.WriteLine($"\nSeeded organizer : {organizer.Name} (Id={organizer.Id})");
    Console.WriteLine($"Seeded attendee  : {attendee.FullName} (Id={attendee.Id})");
    Console.WriteLine($"Full address     : {attendee.FullAddress}");
}
else
{
    Console.WriteLine("\nDatabase already contains data — skipping seed.");
}

Console.WriteLine("\nDone.");
