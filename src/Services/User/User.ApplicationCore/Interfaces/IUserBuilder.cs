using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace User.ApplicationCore.Interfaces
{
    public interface IUserBuilder
    {
        IServiceCollection Services { get; }

        //public void SetDataBase(DataBase dataBase,string connectionString);
        public void UseDataBase(Action<DbContextOptionsBuilder> buildAction);
    }
}
