namespace data.model;

public class Feedback
{
    public  int Id  { get; set; }
    public  int Stars { get; set; }
    public string? Content { get; set; }
    public  DateTime DataOfPublication { get; set; }
    public User? IDUser { get; set; }
    
    public Shop? IDShop { get; set; }
}