using User.ApplicationCore.Entities;
using User.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.ApplicationCore.Service;

public class UserService
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

}
