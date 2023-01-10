using data.model;
using data.Repository;
using logic.Exceptions;

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
    public Page<User> GetUsers()
    {
        return userrepository.GetPage(userrepository.DbSet(), 1, 20);
    }

    public Page<Feedback> GetFeedback(int userId)
    {
        return _feedbackRepositiry.GetFeedbackbyUser(userId);
    }

    public User? GetUser(int id)
    {
        var u = userrepository.GetById(id);
        if (u == null)
        {
            throw new UserNotFoundException();
        }

        return u;
    }
}