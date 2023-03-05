using Base.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using User.ApplicationCore.Interfaces.Services;
using WebAPI.Extensions;
using Entities = User.ApplicationCore.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;
        private ICacheFactory _cacheFactory;
        private readonly User.Infrastructure.UserOptions _options;
        private IMemoryCache _cache;

        public UserController(ILogger<UserController> logger, IUserService service, ICacheFactory cacheFactory, IOptions<User.Infrastructure.UserOptions> optionsAccessor)
        {
            _logger = logger;
            _service = service;
            _cacheFactory = cacheFactory;
            _options = optionsAccessor?.Value ?? new User.Infrastructure.UserOptions();

            if (_options.EnableCache)
            {
                _cache = cacheFactory.GetOrCreateCache(Constants.UserCacheKey);
                _cacheFactory = cacheFactory;
            }
        }

        [HttpGet]
        public IEnumerable<Entities.User> GetUsers()
        {
            if (!_options.EnableCache)
                return _service.GetUsers().ToList();

            var data = _cache.GetCache("User-GetUsers");
            if (data == null)
            {
                data = _service.GetUsers().ToList();
                _cache.SetCache("User-GetUsers", data, _options.CacheOptions);
            }
            return (IEnumerable<Entities.User>)data;
        }

        [HttpGet]
        [Route("{id}")]
        public Entities.User GetUser(int id)
        {
            var user = _service.GetUser(id);
            return user;
        }

        [HttpGet]
        [Route("query")]
        public IEnumerable<Entities.User> GetByQuery()
        {
            //var user = _context.User.FromSqlRaw($"select * from user where id in(1,2)");
            Expression<Func<Entities.User, bool>> express = a => a.gender == "male";
            var user = _service.SearchCondition(express);
            return user.ToList();
        }

        [HttpGet]
        [Route("name/{name}")]
        public Entities.User GetUserByName(String name)
        {
            Expression<Func<Entities.User, bool>> express = a => a.name == name;
            var user = _service.SearchCondition(express);
            return user.FirstOrDefault();
        }

        [HttpGet]
        [Route("update")]
        public bool UpdateUser()
        {
            var user = _service.GetUser(3);
            user.name = "懒羊羊";
            return _service.Update(user);
        }

        [HttpGet]
        [Route("insert")]
        public bool InsertUser()
        {
            var user = new Entities.User();
            user.name = "test";
            user.gender = "male";
            user.password = "123";
            user.age = 100;
            return _service.Insert(user);
        }

        [HttpGet]
        [Route("delete")]
        public bool DeleteUser()
        {
            var user = _service.GetUser(9);
            return _service.Delete(user);
        }



    }
}
