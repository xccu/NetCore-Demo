using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using User.ApplicationCore.Interfaces.Repositories;
using User.ApplicationCore.Interfaces.Services;
using User.Infrastructure.Data;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;



        //方式1：注入MySqlContext
        //private readonly UserContext _context;

        //public UserController(ILogger<UserController> logger,MySqlContext context)
        //{
        //    _logger = logger;
        //    _context = context;
        //    _repository = new UserRepository(_context);
        //}

        //方式2：注入IUserRepository

        //private readonly IUserRepository _repository;
        //public UserController(ILogger<UserController> logger, IUserRepository repository)
        //{
        //    _logger = logger;
        //    _repository = repository;
        //}


        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<User.ApplicationCore.Entities.User> GetUsers()
        {
            var users = _service.GetUsers();
            return users.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public User.ApplicationCore.Entities.User GetUser(int id)
        {
            var user = _service.GetUser(id);
            return user;
        }

        [HttpGet]
        [Route("query")]
        public IEnumerable<User.ApplicationCore.Entities.User> GetByQuery()
        {
            //var user = _context.User.FromSqlRaw($"select * from user where id in(1,2)");
            Expression<Func<User.ApplicationCore.Entities.User, bool>> express = a => a.gender == "male";
            var user = _service.SearchCondition(express);
            return user.ToList();
        }

        [HttpGet]
        [Route("name/{name}")]
        public User.ApplicationCore.Entities.User GetUserByName(String name)
        {
            Expression<Func<User.ApplicationCore.Entities.User, bool>> express = a => a.name == name;
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
            var user = new User.ApplicationCore.Entities.User();
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
