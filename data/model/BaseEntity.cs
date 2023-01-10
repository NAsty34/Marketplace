namespace data.model;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public int CreatorId { get; set; }
    //public User Creator { get; set; }
    public DateTime? EditDate { get; set; }
    public int? EditorId { get; set; }
    //public User? Editor { get; set; }
    public DateTime? DeletedDate { get; set; }
    public int? DeletorId { get; set; }
    //public User? Deletor { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; }
}