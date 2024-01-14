using System.Collections.Generic;
using System.Linq;
using User.ApplicationCore.Dtos;
using User.ApplicationCore.Interfaces.Repositories;
using User.ApplicationCore.Interfaces.Services;

namespace User.ApplicationCore.Service;

public class CourseService: ICourseService
{
    private ICourseRepository _courseRepository;
    private IUserRepository _userRepository;
    private IUserCourseRepository _userCourseRepository;

    public CourseService(
        ICourseRepository courseRepository, 
        IUserRepository userRepository, 
        IUserCourseRepository userCourseRepository) 
    {
        _courseRepository = courseRepository;
        _userRepository = userRepository;
        _userCourseRepository = userCourseRepository;
    }

    public IEnumerable<Entities.Course> GetCourses()
    {
        return _courseRepository.GetAll();
    }

    public GetCourseByUserDTO GetByUser(string userId) 
    {
        var dto = new GetCourseByUserDTO();

        var course = _courseRepository.GetAll()
           .Join(_userCourseRepository.GetAll(), c => c.Id, uc => uc.CourseId, (c, uc) => new { c, uc })
           .Join(_userRepository.GetAll(), c => c.uc.UserId, u => u.Id, (uc, u) => new { uc, u })
           .Where(t => t.u.Id == userId).Select(t => new { t.uc.uc, t.uc.c,t.u.Name });

        var courseDtos = new List<CourseDTO>();
        foreach (var item in course)
        {
            CourseDTO courseDto = new CourseDTO();
            courseDto.score = item.uc.Score;
            courseDto.courseName = item.c.CourseName;
            courseDto.credit = item.c.Credit;
            courseDtos.Add(courseDto);
        }
        dto.userName = course.FirstOrDefault().Name;
        dto.courses = courseDtos;

        return dto;
    }

}
