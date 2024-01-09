using Base.Infrastructure.Repositories;
using User.ApplicationCore.Interfaces.Repositories;
using User.Infrastructure.Data;

namespace User.Infrastructure.Repositories;

public class UserRepository : BaseRepository<ApplicationCore.Entities.User>, IUserRepository
{
    public UserRepository(UserContext dbContext) : base(dbContext)
    {

    }
}
