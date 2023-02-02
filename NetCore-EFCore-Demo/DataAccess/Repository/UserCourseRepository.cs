using Microsoft.EntityFrameworkCore;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserCourseRepository : BaseRepository<UserCourse>, IUserCourseRepository
    {
        public UserCourseRepository(UserContext dbContext) : base(dbContext)
        {

        }
    }

}
