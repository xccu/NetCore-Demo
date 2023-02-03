using Base.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.ApplicationCore.Entities;
using User.ApplicationCore.Interfaces.Repositories;
using User.Infrastructure.Data;

namespace User.Infrastructure.Repositories;

public class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    private readonly UserContext _context;

    public CourseRepository(UserContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public IQueryable<Course> getByUser(int userId)
    {
        var course = _context.Course
            .Join(_context.UserCourse, c => c.id, uc => uc.courseId, (c, uc) => new { c, uc })
            .Join(_context.User, c => c.uc.userId, u => u.id, (uc, u) => new { uc, u })
            .Where(t => t.u.id == userId).Select(t => t.uc.c) ;

        return course;
    }
}
