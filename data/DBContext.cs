using data.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace data;

public class DBContext:DbContext
{
    private static DBContext _context;
    public object FeedBacks;

    public DBContext()
    {
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
            Role = Role.Admin,
            Email = "admin@gmail.com",
            Password = "0000",
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
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Marketplace;Username=postgres;Password=5131");
    }
    
    public static DBContext GetContext()
    {
        if (_context == null) _context = new DBContext();
        return _context;
    }
    
    //public DbSet<BaseEntity> BaseEntities { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Shop> Shops { get; set; } = null!;
    public DbSet<Feedback> Feedbacks { get; set; } = null!;
}     