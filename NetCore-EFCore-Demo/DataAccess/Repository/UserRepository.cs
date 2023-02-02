using Microsoft.EntityFrameworkCore;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(UserContext dbContext) : base(dbContext)
        {

        }



    }

}
