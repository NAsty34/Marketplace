namespace data.model;

public class FileInfo:BaseEntity
{
    public string Name { get; set; }
    public string Extension { get; set; }
    public int EntityId { get; set; } //id магазина или пользователя, которому принадлежит файл
}