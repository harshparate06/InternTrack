
using InternTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace InternTrack.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<InternshipApplication> Applications
        => Set<InternshipApplication>();
}
