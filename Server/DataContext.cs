global using Microsoft.EntityFrameworkCore;
global using VehicleSpeedControlSystem.Shared.Entities;

namespace VehicleSpeedControlSystem.Server;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<RestrictedArea> RestrictedAreas { get; set; }
	public DbSet<Coordinate> Coordinates { get; set; }
	// public DbSet<Admin> Admins { get; set; }
	// public DbSet<Landlord> Landlords { get; set; }
	// public DbSet<Purchase> Purchases { get; set; }
	// public DbSet<TitleDeed> TitleDeeds { get; set; }
	// public DbSet<Ownership> Ownerships { get; set; }
	public DbSet<User> Users { get; set; }
	// public DbSet<Report> Reports { get; set; }
	// public DbSet<Dispute> Disputes { get; set; }
}
