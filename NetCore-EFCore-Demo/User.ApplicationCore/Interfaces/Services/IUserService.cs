using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using User.ApplicationCore.Interfaces.Repositories;

namespace User.ApplicationCore.Interfaces.Services
{
    public interface IUserService
    {
        public IEnumerable<Entities.User> GetUsers();
        public Entities.User GetUser(int id);
        public IEnumerable<Entities.User> SearchCondition(Expression<Func<Entities.User, bool>> expression);
        public bool Update(Entities.User user);
        public bool Insert(Entities.User user);
        public bool Delete(Entities.User user);
        public bool Delete(int id);
    }
}
