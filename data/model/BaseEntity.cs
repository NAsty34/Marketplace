using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.model;

public class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid? CreatorId { get; set; }
    //public User Creator { get; set; }
    public DateTime? EditDate { get; set; }
    public Guid? EditorId { get; set; }
    //public User? Editor { get; set; }
    public DateTime? DeletedDate { get; set; }
    public Guid? DeletorId { get; set; }
    //public User? Deletor { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; }
}