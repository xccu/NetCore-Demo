using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository<T>
    {
        bool Insert(T t);
        bool Update(T t);
        bool Delete(T t);
        IQueryable<T> GetAll();
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
        IQueryable<T> GetBySql(String sql);
        T GetById(object id);
        bool IsExist(object id);
        int GetCount(T t);
    }
}
