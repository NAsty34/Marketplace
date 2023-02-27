using data.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace data;

public class MarketplaceContext : DbContext
{
    private readonly IConfiguration _appConfig;

    public MarketplaceContext(DbContextOptions options, IConfiguration appConfig) : base(options)
    {
        _appConfig = appConfig;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<UserEntity>(a =>
        {
            a.HasMany(ufs => ufs.FavoriteShops)
                .WithMany(shopuser => shopuser.Users)
                .UsingEntity(userfavoriteshop => userfavoriteshop.ToTable(("FavoriteShops")));
            a.HasQueryFilter(statusdeleted => !statusdeleted.IsDeleted);
            
        });
        
        modelBuilder.Entity<ShopEntity>(a =>
        {
            a.HasOne(creatorshop => creatorshop.Creator)
                .WithMany(shop => shop.Shops)
                .HasForeignKey(idcreaatorshop => idcreaatorshop.CreatorId);
            a.HasQueryFilter(statusdeleted => !statusdeleted.IsDeleted);
        });

        modelBuilder.Entity<ProductEntity>(a =>
        {
          a.HasQueryFilter(statusdeleted => !statusdeleted.IsDeleted);
        });
        modelBuilder.Entity<TypeEntity>(a =>
        {
            a.HasQueryFilter(statusdeleted => !statusdeleted.IsDeleted);
        });
        /*modelBuilder.Entity<CategoryEntity>(a =>
        {
            a.HasQueryFilter(statusdeleted => !statusdeleted.IsDeleted);
        });*/
        modelBuilder.Entity<PaymentMethodEntity>(a =>
        {
            a.HasQueryFilter(statusdeleted => !statusdeleted.IsDeleted);
        });
        modelBuilder.Entity<DeliveryTypeEntity>(a =>
        {
            a.HasQueryFilter(statusdeleted => !statusdeleted.IsDeleted);
        });
        modelBuilder.Entity<FeedbackEntity>(a =>
        {
            a.HasQueryFilter(statusdeleted => !statusdeleted.IsDeleted);
        });
        
        modelBuilder.Entity<ShopCategoryEntity>(a => { a.HasKey(u => new { shopid = u.ShopEntityId, u.CategoryId }); });

        modelBuilder.Entity<ShopTypesEntity>(a => { a.HasKey(u => new { shopid = u.ShopEntityId, u.TypeId }); });

        modelBuilder.Entity<ShopDeliveryEntity>(a => { a.HasKey(u => new { shopid = u.ShopEntityId, u.DeliveryId }); });

        modelBuilder.Entity<ShopPaymentEntity>(a => { a.HasKey(u => new { shopid = u.ShopEntityId, Paymentid = u.PaymentId }); });

        modelBuilder.Entity<UserEntity>().Property(d => d.RoleEntity).HasConversion(new EnumToStringConverter<RoleEntity>());
        modelBuilder.Entity<ProductEntity>().Property(a => a.Country)
            .HasConversion(new EnumToStringConverter<CountryEntity>());
        
        base.OnModelCreating(modelBuilder);
    }


    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<ShopEntity> Shops { get; set; } = null!;
    public DbSet<FeedbackEntity> Feedbacks { get; set; } = null!;
    public DbSet<FileInfoEntity> FileInfos { get; set; } = null!;
    public DbSet<CategoryEntity> Categories { get; set; } = null!;
    public DbSet<DeliveryTypeEntity> DeliveryTypes { get; set; } = null!;
    public DbSet<TypeEntity> Types { get; set; } = null!;
    public DbSet<PaymentMethodEntity> PaymentMethods { get; set; } = null!;
    public DbSet<ShopCategoryEntity> ShopCategories { get; set; } = null!;
    public DbSet<ShopDeliveryEntity> ShopDeliveries { get; set; } = null!;
    public DbSet<ShopPaymentEntity> ShopPayments { get; set; } = null!;
    public DbSet<ShopTypesEntity> ShopTypes { get; set; } = null!;
    public DbSet<ProductEntity> Product { get; set; } = null!;
}