using System.ComponentModel.DataAnnotations.Schema;

namespace data.model;

public class Category:DictionaryBase
{
    public virtual Category? parent { get; set; }
    
}