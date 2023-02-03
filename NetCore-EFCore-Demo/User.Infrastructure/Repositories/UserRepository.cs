using Base.Infrastructure.Repositories;
using User.ApplicationCore.Interfaces.Repositories;

namespace User.Infrastructure.Data;

public class UserRepository : BaseRepository<ApplicationCore.Entities.User>, IUserRepository
{
    public UserRepository(UserContext dbContext) : base(dbContext)
    {

    }
}
