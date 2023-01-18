using data.model;
using data.Repository;
using logic.Exceptions;
using logic.Service.Inreface;

namespace logic.Service;

public class UserServer:IUserServer
{
    private readonly IRepositoryUser userrepository;
    private readonly IHashService _hashService;
    

    public UserServer(IRepositoryUser userrepository, IHashService hashService)
    {
        this.userrepository = userrepository;
        this._hashService = hashService;
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

    public User CreateAdmin(User user)
    {
        if (userrepository.GetUser(user.Email) != null)
        {
            throw new EmailException();
        }

        user.Password = _hashService.Hash(user.Password);
        userrepository.Create(user);
        userrepository.Save();
        return user;
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