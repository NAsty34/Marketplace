namespace logic.Exceptions;

public class FeedbackNotFoundException:Exception
{
    public FeedbackNotFoundException():base("Feedback not found"){}
}