
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using User.ApplicationCore.Dtos;
using User.ApplicationCore.Service;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private readonly CourseService _service;

        public CourseController(ILogger<CourseController> logger, CourseService service)
        {
            _logger = logger;
            _service = service;
        }

        //https://localhost:5001/course
        [HttpGet]
        public IEnumerable<object> GetCourse()
        {
            return _service.GetCourses() ;
        }

        //https://localhost:5001/course/user/1
        [HttpGet]
        [Route("user/{userId}")]
        public GetCourseByUserDTO GetCourseByUser(int userId)
        {
            var courseDto = _service.GetByUser(userId);
            return courseDto;
        }
    }
}
