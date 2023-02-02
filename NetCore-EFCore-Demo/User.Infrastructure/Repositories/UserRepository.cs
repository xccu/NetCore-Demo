using Base.Infrastructure.Repositories;
using User.ApplicationCore.Interfaces;

namespace User.Infrastructure.Data;

public class UserRepository : BaseRepository<ApplicationCore.Entities.User>, IUserRepository
{
    public UserRepository(UserContext dbContext) : base(dbContext)
    {

    }
}
