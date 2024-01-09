using Base.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.ApplicationCore.Entities;
using User.ApplicationCore.Interfaces.Repositories;

namespace User.Infrastructure.Data;

public class UserCourseRepository : BaseRepository<UserCourse>, IUserCourseRepository
{
    public UserCourseRepository(UserContext dbContext) : base(dbContext)
    {

    }
}
