using data.model;

namespace Marketplace.DTO;

public class FeedbackDto
{
    public FeedbackDto()
    {
    }

    public FeedbackDto(FeedbackEntity feedbackEntity)
    {
        Id = feedbackEntity.Id;
        Stars = feedbackEntity.Stars;
        Content = feedbackEntity.Content;
        isBlock = !feedbackEntity.IsActive;
        Creator = new UserDto(feedbackEntity.Creator);
        Shop = new ShopDto(feedbackEntity.Shop);
    }
    public Guid Id { get; set; }
    public  int Stars { get; set; }
    public string? Content { get; set; }
    public UserDto Creator { get; set; } = null!;
    public ShopDto Shop { get; set; } = null!;
    public  bool isBlock { get; set; }
}