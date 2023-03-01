using data.model;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;

namespace logic.Service;

public class UserServer:IUserServer
{
    private readonly IUserRepository _userrepository;
    private readonly IHashService _hashService;

    public UserServer(IUserRepository userrepository, IHashService hashService)
    {
        _userrepository = userrepository;
        _hashService = hashService;
    }
    public async Task<PageEntity<UserEntity>> GetUsers(int? page, int? size)
    {
        return await _userrepository.GetPage(page, size);
    }

    public async Task<UserEntity> GetUser(Guid id)
    {
        var userid = await _userrepository.GetById(id);
        if (userid == null)
        {
            throw new UserNotFoundException();
        }

        return userid;
    }

    public async Task<UserEntity> EditUser(UserEntity userEntity)
    {
        var fromdb = await GetUser(userEntity.Id);
        if (fromdb == null)
        {
            throw new UserNotFoundException();
        }

        fromdb.Name = userEntity.Name;
        fromdb.Surname = userEntity.Surname;
        fromdb.Patronymic = userEntity.Patronymic;
        await _userrepository.Save();
        return fromdb;
    }

    public async Task CreateAdmin(UserEntity userEntity)
    {
        if (_userrepository.GetUser(userEntity.Email!) != null)
        {
            throw new EmailException();
        }

        userEntity.Password = _hashService.Hash(userEntity.Password);
        await _userrepository.Create(userEntity);
        await _userrepository.Save();
        
    }

    public async Task<UserEntity> ChangeBlockUser(Guid id, bool value)
    {
        var userid = await _userrepository.GetById(id);
        if (userid == null)
        {
            throw new UserNotFoundException();
        }

        userid.IsActive = value;
        await _userrepository.Save();
        return userid;
    }
   
}