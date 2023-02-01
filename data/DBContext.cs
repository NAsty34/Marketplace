using data.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace data;

public class DBContext:DbContext
{
    private readonly IConfiguration appConfig;
    
    public DBContext(DbContextOptions options, IConfiguration appConfig) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = Guid.NewGuid(),
            Name = "Admin",
            Patronymic = "Admin",
            Surname = "Admin",
            Role = Role.Admin, EmailIsVerified = true,
            Email = "admin@gmail.com",
            Password = BCrypt.Net.BCrypt.HashPassword(appConfig == null ? "0000" : appConfig["AdminPassword"]),
            CreateDate = DateTime.Now
        });
        modelBuilder.Entity<User>(a =>
        {
            a.HasMany(u => u.FavoriteShops)
                .WithMany(s => s.Users)
                .UsingEntity(f => f.ToTable(("FavoriteShops")));
            a.HasQueryFilter(p => !p.IsDeleted);
        });

        modelBuilder.Entity<Shop>(a =>
        {
            a.HasOne(e => e.Creator)
                .WithMany(e => e.Shops)
                .HasForeignKey(k => k.CreatorId);
            a.HasQueryFilter(p => !p.IsDeleted);
        });
            
        modelBuilder.Entity<User>().Property(d => d.Role).HasConversion(new EnumToStringConverter<Role>());
        base.OnModelCreating(modelBuilder);
    }
    
    

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Shop> Shops { get; set; } = null!;
    public DbSet<Feedback> Feedbacks { get; set; } = null!;
    public DbSet<data.model.FileInfo> FileInfos { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<DeliveryType> DeliveryTypes { get; set; } = null!;
    public DbSet<data.model.Type> Types { get; set; } = null!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
    public DbSet<ShopCategory> ShopCategories { get; set; } = null!;
    public DbSet<ShopDelivery> ShopDeliveries { get; set; } = null!;
    public DbSet<ShopPayment> ShopPayments { get; set; } = null!;
    public DbSet<ShopTypes> ShopTypes { get; set; } = null!;
}     