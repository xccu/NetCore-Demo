using DTO;
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
    public class CourseControllerTest
    {

        private readonly TestServer _server;
        private readonly HttpClient _client;

        public CourseControllerTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            _client = _server.CreateClient();
        }


        //[Fact]
        public async Task GetCourseByUserTest()
        {
            var response = await _client.GetAsync("/course/user/1");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            IList<GetCourseByUserDTO> users = JsonConvert.DeserializeObject<IList<GetCourseByUserDTO>>(responseString);

            Assert.Equal(0, users.Count);
        }

    }
}
