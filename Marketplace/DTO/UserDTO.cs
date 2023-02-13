using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using data.model;
namespace Marketplace.DTO;

public class UserDto
{
    public UserDto()
    {
        
    }
    public UserDto(User _user)
    {
        Id = _user.Id;
        Name = _user.Name;
        Patronymic = _user.Patronymic;
        Email = _user.Email;
        Surname = _user.Surname;
        role = _user.Role;
    }
    public string Name { get; set; }
    public  string Surname { get; set; }
    public  string Patronymic { get; set; }
    [EmailAddress]
    public  string Email { get; set; }
    public Role role { get; set; }
    public Guid Id { get; set; }
}