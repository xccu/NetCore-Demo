using DataAccess.Repository;
using MockData;
using Service;
using System;
using Xunit;

namespace Test
{
    public class UserServiceTest
    {
        UserService _service;

        public UserServiceTest()
        {
            var userRepository = new UserRepository(MockMySqlContext.GetContext());
            _service = new UserService(userRepository);
        }

        [Fact]
        public void GetUsersTest()
        {
            var user = _service.GetUsers();
            Assert.NotNull(user);
        }

        [Fact]
        public void GetUserTest()
        {
            var user = _service.GetUser(1);
            Assert.Equal("œ≤—Ú—Ú",user.name);
        }
    }
}