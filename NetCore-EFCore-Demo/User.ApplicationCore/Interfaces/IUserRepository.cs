
using Base.ApplicationCore.Interfaces;
using User.ApplicationCore.Entities;

namespace User.ApplicationCore.Interfaces;

public interface IUserRepository : IRepository<Entities.User>
{
}
