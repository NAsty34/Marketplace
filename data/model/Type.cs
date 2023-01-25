using System.ComponentModel.DataAnnotations;

namespace data.model;

public class Type:DictionaryBase
{ 
    [MaxLength(500)]
    public string description { get; set; }
}