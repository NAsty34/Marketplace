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

   

    public User? GetUser(int id)
    {
        var userid = userrepository.GetById(id);
        if (userid == null)
        {
            throw new UserNotFoundException();
        }

        return userid;
    }

    public User EditUser(User user)
    {
        var fromdb = GetUser(user.Id);
        if (fromdb == null)
        {
            throw new UserNotFoundException();
        }

        fromdb.Name = user.Name;
        fromdb.Surname = user.Surname;
        fromdb.Patronymic = user.Patronymic;
        userrepository.Save();
        return fromdb;
    }

    public User ChangeBlockUser(int id, bool value)
    {
        var userid = userrepository.GetById(id);
        if (userid == null)
        {
            throw new UserNotFoundException();
        }

        userid.IsActive = value;
        userrepository.Save();
        return userid;
    }
}