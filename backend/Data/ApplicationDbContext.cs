using Microsoft.EntityFrameworkCore;
using SiteLocaVelos.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<Velo> Velos { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
}