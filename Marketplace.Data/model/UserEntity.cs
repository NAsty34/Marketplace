using System.ComponentModel.DataAnnotations;

namespace data.model;

public class UserEntity:BaseEntity
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Surname { get; set; }
    [Required]
    public string? Patronymic { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public RoleEntity RoleEntity { get; set; }
    [Required]
    public string Password { get; set; } = null!;

    public virtual List<ShopEntity> FavoriteShops { get; set; } = new();
    
    public virtual List<ShopEntity> Shops { get; set; } = new();
    public bool EmailIsVerified { get; set; }
    public string? EmailCode { get; set; }
}