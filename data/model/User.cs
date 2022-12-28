using System.Data;
using System.Runtime.InteropServices.JavaScript;

namespace data.model;

public class User
{
    public  int Id { get; set; }
    public string? Name { get; set; }
    public  string? Surname { get; set; }
    public  string? Patronymic { get; set; }
    public  string? Email { get; set; }
    public  Role Role { get; set; }
    public  string? Password { get; set; }
    public  DateTime DataRegistration { get; set; }
}