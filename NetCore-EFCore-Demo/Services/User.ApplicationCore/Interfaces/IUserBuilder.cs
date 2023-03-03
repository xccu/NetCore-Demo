using Base.Infrastructure.Enum;
using Microsoft.Extensions.DependencyInjection;

namespace User.ApplicationCore.Interfaces
{
    public interface IUserBuilder
    {
        IServiceCollection Services { get; }

        public void UseDataBase(DataBase dataBase,string connectionString);
    }
}
