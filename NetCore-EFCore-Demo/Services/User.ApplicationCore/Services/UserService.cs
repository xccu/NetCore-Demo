using User.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using User.ApplicationCore.Interfaces.Repositories;
using User.ApplicationCore.Interfaces.Services;
using System.Linq.Expressions;

namespace User.ApplicationCore.Service;

public class UserService : IUserService
{
    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<Entities.User> GetUsers()
    {
        return _userRepository.GetAll();
    }

    public Entities.User GetUser(int id)
    {
        return _userRepository.GetById(id);
    }

    public IEnumerable<Entities.User> SearchCondition(Expression<Func<Entities.User, bool>> expression)
    {
        return _userRepository.GetByCondition(expression);
    }

    public bool Update(Entities.User user)
    {
        return _userRepository.Update(user);
    }

    public bool Insert(Entities.User user)
    {
        return _userRepository.Insert(user);
    }

    public bool Delete(Entities.User user)
    {
        return _userRepository.Delete(user);
    }

    public bool Delete(int id)
    {
        return _userRepository.Delete(id);
    }
}
