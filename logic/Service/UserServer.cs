using data.model;
using data.Repository;

namespace logic.Service;

public class UserServer:IUserServer
{
    private readonly IRepositoryUser userrepository;
    private readonly IFeedbackRepositiry _feedbackRepositiry;

    public UserServer(IRepositoryUser userrepository, IFeedbackRepositiry _feedbackRepositiry)
    {
        this.userrepository = userrepository;
        this._feedbackRepositiry = _feedbackRepositiry;
    }
    public IEnumerable<User> GetUsers()
    {
        return userrepository.GetUsers();
    }

    public IEnumerable<Feedback> GetFeedback(int userId)
    {
        return _feedbackRepositiry.GetFeedbackbyUser(userId);
    }

    public User? GetUser(int id)
    {
        var u = userrepository.GetUserId(id);
        if (u == null)
        {
            throw new SystemException("User not found");
        }

        return u;
    }
}