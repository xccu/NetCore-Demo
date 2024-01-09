using User.ApplicationCore.Interfaces.Repositories;
using User.ApplicationCore.Interfaces.Services;
using System.Linq.Expressions;
using Base.ApplicationCore.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using Base.ApplicationCore;


namespace User.ApplicationCore.Service;


public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private ICacheFactory _cacheFactory;
    private readonly Entities.UserOptions _options;
    private IMemoryCache _cache;

    public UserService(IUserRepository userRepository, ICacheFactory cacheFactory, IOptions<Entities.UserOptions> optionsAccessor)
    {
        _userRepository = userRepository;

        _cacheFactory = cacheFactory;
        _options = optionsAccessor?.Value ?? new Entities.UserOptions();

        if (_options.EnableCache)
        {
            _cache = cacheFactory.GetOrCreateCache(Constants.UserCacheKey);
            _cacheFactory = cacheFactory;
        }
    }

    public IEnumerable<Entities.User> GetUsers()
    {
        if (!_options.EnableCache)
            return _userRepository.GetAll();

        var data = _cache.GetCache("User.GetUsers");
        if (data == null)
        {
            data = _userRepository.GetAll().ToList();
            _cache.SetCache("User.GetUsers", data, _options.CacheOptions);
        }
        return (IEnumerable<Entities.User>)data;
    }

    public Entities.User GetUser(int id)
    {
        if (!_options.EnableCache)
            return _userRepository.GetById(id);

        var data = _cache.GetCache($"User.GetUser.{id}");
        if (data == null)
        {
            data = _userRepository.GetById(id);
            _cache.SetCache($"User.GetUser.{id}", data, _options.CacheOptions);
        }
        return (Entities.User)data;
    }

    public IEnumerable<Entities.User> SearchCondition(Expression<Func<Entities.User, bool>> expression)
    {        
        return _userRepository.GetByCondition(expression);
    }

    public bool Update(Entities.User user)
    {
        RefreshCache();
        return _userRepository.Update(user);
    }

    public bool Insert(Entities.User user)
    {
        RefreshCache();
        return _userRepository.Insert(user);
    }

    public async Task<bool> InsertAsync(Entities.User user)
    {
        RefreshCache();
        return await _userRepository.InsertAsync(user);
    }

    public bool Delete(Entities.User user)
    {
        RefreshCache();
        return _userRepository.Delete(user);
    }

    public bool Delete(int id)
    {
        RefreshCache();
        return _userRepository.Delete(id);       
    }


    private void RefreshCache()
    {
        if (_options.EnableCache)
        {
            _cache = _cacheFactory.Refresh(Constants.UserCacheKey);
        }
    }
}
