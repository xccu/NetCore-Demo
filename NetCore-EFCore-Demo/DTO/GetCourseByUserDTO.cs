using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class GetCourseByUserDTO
    {
        public GetCourseByUserDTO()
        {
            courses = new List<CourseDTO>();
        }

        public string userName { get; set; }
        public List<CourseDTO> courses { get; set; }

    }
}
