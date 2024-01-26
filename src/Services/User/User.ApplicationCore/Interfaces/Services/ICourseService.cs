using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.ApplicationCore.Dtos;
using User.ApplicationCore.Interfaces.Repositories;

namespace User.ApplicationCore.Interfaces.Services
{
    public interface ICourseService
    {
        public IEnumerable<Entities.Course> GetCourses();
        public GetCourseByUserDTO GetByUser(string userId);
    }
}
