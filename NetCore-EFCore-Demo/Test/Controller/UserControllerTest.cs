using DataAccess.Repository;
using Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebAPI;
using Xunit;

namespace Test.Controller
{
    public class UserControllerTest
    {

        private readonly TestServer _server;
        private readonly HttpClient _client;

        public UserControllerTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetUsersTestAsync()
        {
            var response = await _client.GetAsync("/user");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            IList<User> users = JsonConvert.DeserializeObject<IList<User>>(responseString);

            Assert.Equal(2, users.Count);
        }

        [Fact]
        public async Task GetUserTestAsync()
        {
            var response = await _client.GetAsync("/user/1");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(responseString);

            Assert.Equal("喜羊羊", user.name);
        }
    }

}
