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
            a.HasMany(ufs => ufs.FavoriteShops)
                .WithMany(shopuser => shopuser.Users)
                .UsingEntity(userfavoriteshop => userfavoriteshop.ToTable(("FavoriteShops")));
            a.HasQueryFilter(statusdeleted => !statusdeleted.IsDeleted);
        });

        modelBuilder.Entity<Shop>(a =>
        {
            a.HasOne(creatorshop => creatorshop.Creator)
                .WithMany(shop => shop.Shops)
                .HasForeignKey(idcreaatorshop => idcreaatorshop.CreatorId);
            a.HasQueryFilter(statusdeleted => !statusdeleted.IsDeleted);
        });

        modelBuilder.Entity<ShopCategory>(a =>
        {
            a.HasKey(u => new{u.shopid, u.CategoryId});
        });
            
        modelBuilder.Entity<ShopTypes>(a =>
        {
            a.HasKey(u => new{u.shopid, u.TypeId});
        });
        
        modelBuilder.Entity<ShopDelivery>(a =>
        {
            a.HasKey(u => new{u.shopid, u.DeliveryId});
        });
        
        modelBuilder.Entity<ShopPayment>(a =>
        {
            a.HasKey(u => new{u.shopid, u.Paymentid});
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