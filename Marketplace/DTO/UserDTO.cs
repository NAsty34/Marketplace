using System.ComponentModel;
using System.Text.Json.Serialization;
using data.model;
namespace Marketplace.DTO;

public class UserDto
{
    public string Name { get; set; }
    public  string Surname { get; set; }
    public  string Patronymic { get; set; }
    public  string Email { get; set; }
    public Role role { get; set; }
}