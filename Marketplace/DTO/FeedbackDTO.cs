using data.model;

namespace Marketplace.DTO;

public class FeedbackDTO
{
    public FeedbackDTO()
    {
    }

    public FeedbackDTO(Feedback _feedback, IConfiguration appConfig)
    {
        Id = _feedback.Id;
        Stars = _feedback.Stars;
        Content = _feedback.Content;
        Creator = new UserDto(_feedback.Creator);
        Shop = new ShopDTO(_feedback.Shop, appConfig);
    }
    public Guid Id { get; set; }
    public  int Stars { get; set; }
    public string? Content { get; set; }
    public UserDto Creator { get; set; }
    public ShopDTO Shop { get; set; }
}