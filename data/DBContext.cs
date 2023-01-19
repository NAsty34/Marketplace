using System.Xml;
using data.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace data;

public class DBContext:DbContext
{
    private readonly IConfiguration appConfig;
    public DBContext(IConfiguration _appConfig)
    {
        this.appConfig = _appConfig;
        Database.EnsureCreated();
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Name = "Admin",
            Patronymic = "Admin",
            Surname = "Admin",
            Role = Role.Admin, EmailIsVerified = true,
            Email = "admin@gmail.com",
            Password = BCrypt.Net.BCrypt.HashPassword(appConfig["AdminPassword"]),
            CreateDate = DateTime.Now
        });
        modelBuilder.Entity<User>()
            .HasMany(u => u.FavoriteShops)
            .WithMany(s => s.Users)
            .UsingEntity(f => f.ToTable(("FavoriteShops")));
        modelBuilder.Entity<Shop>()
            .HasOne(e => e.Creator)
            .WithMany(e => e.Shops)
            .HasForeignKey(k => k.CreatorId);
        modelBuilder.Entity<User>().Property(d => d.Role).HasConversion(new EnumToStringConverter<Role>());
        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        optionsBuilder.UseNpgsql(appConfig["ConnectionString"]);
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Shop> Shops { get; set; } = null!;
    public DbSet<Feedback> Feedbacks { get; set; } = null!;
}     