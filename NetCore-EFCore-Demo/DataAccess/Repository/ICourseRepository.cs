using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICourseRepository : IRepository<Course>
    {
        IQueryable<Course> getByUser(int userId);
    }
}
