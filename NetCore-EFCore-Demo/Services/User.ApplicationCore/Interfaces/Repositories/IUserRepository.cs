
using Base.ApplicationCore.Interfaces;
using User.ApplicationCore.Entities;

namespace User.ApplicationCore.Interfaces.Repositories;

public interface IUserRepository : IRepository<Entities.User>
{
}
