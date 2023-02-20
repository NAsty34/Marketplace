using data.model;
namespace Marketplace.DTO;

public class UserDto
{
    public UserDto()
    {
        
    }
    public UserDto(User user)
    {
        Id = user.Id;
        Name = user.Name;
        Patronymic = user.Patronymic;
        Email = user.Email;
        Surname = user.Surname;
        Role = user.Role;
    }
    public string Name { get; set; }
    public  string Surname { get; set; }
    public  string Patronymic { get; set; }
    
    public  string Email { get; set; }
    public Role Role { get; set; }
    public Guid Id { get; set; }
}