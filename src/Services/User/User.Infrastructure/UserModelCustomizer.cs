using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using User.ApplicationCore.Entities;

namespace User.Infrastructure;

//https://blog.darkloop.com/post/using-entity-framework-core-imodelcustomizer-to-target-multiple-databases

public class UserModelCustomizer : IModelCustomizer
{
    public void Customize(ModelBuilder modelBuilder, DbContext context)
    {
        modelBuilder.Entity<ApplicationCore.Entities.User>();
        modelBuilder.Entity<Course>();
        modelBuilder.Entity<UserCourse>().HasKey(t => new { t.userId, t.courseId });
    }
}
