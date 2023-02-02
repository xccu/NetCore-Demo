using DataAccess.Repository;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class UserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUser(int id)
        {
            return _userRepository.GetById(id);
        }

    }
}
