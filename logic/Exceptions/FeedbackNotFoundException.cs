namespace logic.Exceptions;

public class FeedbackNotFoundException:BaseException
{
    public FeedbackNotFoundException():base("Feedback not found", 4, 404){}
}