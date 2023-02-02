﻿using DataAccess.Repository;
using DTO;
using Entity;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class CourseService
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

        public IEnumerable<Course> GetCourses()
        {
            return _courseRepository.GetAll();
        }

        public GetCourseByUserDTO GetByUser(int userId) 
        {
            var dto = new GetCourseByUserDTO();

            var course = _courseRepository.GetAll()
               .Join(_userCourseRepository.GetAll(), c => c.id, uc => uc.courseId, (c, uc) => new { c, uc })
               .Join(_userRepository.GetAll(), c => c.uc.userId, u => u.id, (uc, u) => new { uc, u })
               .Where(t => t.u.id == userId).Select(t => new { t.uc.uc, t.uc.c,t.u.name });

            var courseDtos = new List<CourseDTO>();
            foreach (var item in course)
            {
                CourseDTO courseDto = new CourseDTO();
                courseDto.score = item.uc.score;
                courseDto.courseName = item.c.courseName;
                courseDto.credit = item.c.credit;
                courseDtos.Add(courseDto);
            }
            dto.userName = course.FirstOrDefault().name;
            dto.courses = courseDtos;

            return dto;
        }



    }
}
