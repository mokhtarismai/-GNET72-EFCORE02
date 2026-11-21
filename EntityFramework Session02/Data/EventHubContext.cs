using Microsoft.EntityFrameworkCore;

namespace EntityFramework_Session02;

public class EventHubContext : DbContext
{
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<OrganizerProfile> OrganizerProfiles { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    public DbSet<Badge> Badges { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server = .; Database = EventHubDb; Trusted_Connection = True; TrustServerCertificate = True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfiguration(new OrganizerProfileConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());

        
        modelBuilder.Entity<Badge>()
                    .HasKey(b => b.BadgeNumber);

        modelBuilder.Entity<Badge>()
                    .Property(b => b.IssuedDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Badge>()
                    .Property(b => b.Tier)
                    .IsRequired();

        
        modelBuilder.Entity<Badge>()
                    .HasOne(b => b.Attendee)
                    .WithOne(a => a.Badge)
                    .HasForeignKey<Badge>(b => b.AttendeeId)
                    .OnDelete(DeleteBehavior.Cascade);
    }
}