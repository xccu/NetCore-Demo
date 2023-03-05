using Base.Infrastructure.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.ApplicationCore.Interfaces;
using User.Infrastructure.Data;

namespace User.Infrastructure;

public class UserBuilder : IUserBuilder
{
    public IServiceCollection Services { get; private set; }

    public UserBuilder(IServiceCollection services) => Services = services;

    public void SetDataBase(DataBase dataBase,string connectionString)
    {
        switch (dataBase)
        {
            case DataBase.SqlServer: this.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString)); break;
            case DataBase.Oracle: this.Services.AddDbContext<UserContext>(options => options.UseOracle(connectionString)); break;
            case DataBase.MySql: this.Services.AddDbContext<UserContext>(options => options.UseMySQL(connectionString)); break;
            default: break;
        }        
    }
}
