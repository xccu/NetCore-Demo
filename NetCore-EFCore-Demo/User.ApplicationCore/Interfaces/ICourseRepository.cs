
using Base.ApplicationCore.Interfaces;
using User.ApplicationCore.Entities;

namespace User.ApplicationCore.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    IQueryable<Course> getByUser(int userId);
}
