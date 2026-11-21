namespace EntityFramework_Session02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new EventHubContext();

            // Organizer + Profile + Events (saved together)
            var organizer = new Organizer
            {
                Name = "Mokhtar Ismail",
                CompanyName = "Nile Events",
                IsVerified = true,
                Profile = new OrganizerProfile
                {
                    Bio = "We organize tech conferences.",
                    WebsiteUrl = "https://nile-events.example",
                    LogoUrl = "logo.png"
                },
                Events = new List<Event>
                {
                    new Event
                    {
                        Title = "EF Core Workshop",
                        Description = "A full day workshop about Entity Framework Core.",
                        StartDate = new DateTime(2026, 12, 10, 10, 0, 0),
                        EndDate = new DateTime(2026, 12, 10, 17, 0, 0),
                        MaxAttendees = 100
                    },
                    new Event
                    {
                        Title = "ASP.NET Meetup",
                        Description = "Open meetup, no end date.",
                        StartDate = new DateTime(2026, 12, 20, 18, 0, 0),
                        MaxAttendees = 50
                    }
                }
            };
            context.Organizers.Add(organizer);

            // Attendee + Badge
            var attendee = new Attendee
            {
                FullName = "Sara Mohamed",
                Email = "sara@email.com",
                Street = "15 Farook St",
                City = "Zagazig",
                Country = "Egypt",
                PostalCode = "44519",
                Badge = new Badge
                {
                    IssuedDate = DateTime.Now,
                    Tier = BadgeTier.VIP
                }
            };
            context.Attendees.Add(attendee);

            context.SaveChanges();

            Console.WriteLine($"Organizer #{organizer.Id}: {organizer.Name} - Events: {organizer.Events.Count}");
            Console.WriteLine($"Attendee #{attendee.Id}: {attendee.FullName}");
            Console.WriteLine($"Address: {attendee.FullAddress}");
            Console.WriteLine($"Badge #{attendee.Badge.BadgeNumber} ({attendee.Badge.Tier})");
        }
    }
}