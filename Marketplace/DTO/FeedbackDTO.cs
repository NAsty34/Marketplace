using data.model;

namespace Marketplace.DTO;

public class FeedbackDto
{
    public FeedbackDto()
    {
    }

    public FeedbackDto(Feedback feedback, IConfiguration appConfig)
    {
        Id = feedback.Id;
        Stars = feedback.Stars;
        Content = feedback.Content;
        Creator = new UserDto(feedback.Creator);
        Shop = new ShopDto(feedback.Shop, appConfig);
    }
    public Guid Id { get; set; }
    public  int Stars { get; set; }
    public string? Content { get; set; }
    public UserDto Creator { get; set; } = null!;
    public ShopDto Shop { get; set; } = null!;
}