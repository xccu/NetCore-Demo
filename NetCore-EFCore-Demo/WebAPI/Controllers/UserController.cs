using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using User.ApplicationCore.Interfaces;
using User.Infrastructure.Data;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _repository;
        private readonly UserContext _context;

        //方式1：注入MySqlContext
        //public UserController(ILogger<UserController> logger,MySqlContext context)
        //{
        //    _logger = logger;
        //    _context = context;
        //    _repository = new UserRepository(_context);
        //}

        //方式2：注入IUserRepository
        public UserController(ILogger<UserController> logger, IUserRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<User.ApplicationCore.Entities.User> GetUsers()
        {
            var users = _repository.GetAll();
            return users.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public User.ApplicationCore.Entities.User GetUser(int id)
        {
            var user = _repository.GetById(id);
            return user;
        }

        [HttpGet]
        [Route("query")]
        public IEnumerable<User.ApplicationCore.Entities.User> GetByQuery()
        {
            var user = _context.User.FromSqlRaw($"select * from user where id in(1,2)");
            return user.ToList();
        }

        [HttpGet]
        [Route("name/{name}")]
        public User.ApplicationCore.Entities.User GetUserByName(String name)
        {
            Expression<Func<User.ApplicationCore.Entities.User, bool>> express = a => a.name == name;
            var user = _repository.GetByCondition(express);
            return user.FirstOrDefault();
        }

        [HttpGet]
        [Route("update")]
        public bool UpdateUser()
        {
            var user = _repository.GetById(3);
            user.name = "懒羊羊";
            return _repository.Update(user);
        }

        [HttpGet]
        [Route("insert")]
        public bool InsertUser()
        {
            var user = new User.ApplicationCore.Entities.User();
            user.name = "test";
            user.sex = "male";
            user.password = "123";
            user.age = 100;
            return _repository.Insert(user);
        }

        [HttpGet]
        [Route("delete")]
        public bool DeleteUser()
        {
            var user = _repository.GetById(9);
            return _repository.Delete(user);
        }

    }
}
