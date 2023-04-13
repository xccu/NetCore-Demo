﻿using Base.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using User.ApplicationCore.Entities;
using User.ApplicationCore.Interfaces.Services;
using WebAPI.Extensions;
using Entities = User.ApplicationCore.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;

        }

        [HttpGet]
        public IEnumerable<Entities.User> GetUsers()
        {
            return _service.GetUsers().ToList();
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
