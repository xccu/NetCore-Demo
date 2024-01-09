using Base.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.ApplicationCore.Entities;
using User.ApplicationCore.Interfaces;
using User.ApplicationCore.Interfaces.Repositories;
using User.ApplicationCore.Interfaces.Services;
using User.ApplicationCore.Service;
using User.Infrastructure;
using User.Infrastructure.Data;
using User.Infrastructure.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IUserBuilder AddUser(this IServiceCollection services, Action<IUserBuilder> buildAction,Action<UserOptions> configureAction = null)
    {

        if (configureAction != null)
        {
            services.Configure(configureAction);
        }

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserCourseRepository, UserCourseRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();

        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IUserService>(sp =>
        {
            var cacheFactory = sp.GetService<ICacheFactory>();
            var repository = sp.GetService<IUserRepository>();
            var options = sp.GetService<IOptions<UserOptions>>();

            return new UserService(repository, cacheFactory, options);
        });


        IUserBuilder userbuilder = new UserBuilder(services);
        buildAction?.Invoke(userbuilder);       

        return new UserBuilder(services);
    }

}
