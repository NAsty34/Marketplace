using data.model;
namespace Marketplace.DTO;

public class UserDto
{
    public UserDto()
    {
        
    }
    public UserDto(UserEntity userEntity)
    {
        Id = userEntity.Id;
        Name = userEntity.Name;
        Patronymic = userEntity.Patronymic;
        Email = userEntity.Email;
        Surname = userEntity.Surname;
        Role = userEntity.RoleEntity;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public  string Surname { get; set; }
    public  string Patronymic { get; set; }
    public RoleEntity Role { get; set; }
    public  string Email { get; set; }
    
    
}