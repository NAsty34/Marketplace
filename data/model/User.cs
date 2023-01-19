using System.ComponentModel.DataAnnotations;

namespace data.model;

public class User:BaseEntity
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Surname { get; set; }
    [Required]
    public string? Patronymic { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public Role Role { get; set; }
    [Required]
    public string? Password { get; set; }

    public virtual List<Shop> FavoriteShops { get; set; } = new();
    public virtual List<Shop> Shops { get; set; } = new();
    public bool EmailIsVerified { get; set; }
    public string? EmailCode { get; set; }
}