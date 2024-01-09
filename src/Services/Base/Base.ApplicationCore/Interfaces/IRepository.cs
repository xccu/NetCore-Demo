//using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Base.ApplicationCore.Interfaces;

public interface IRepository<T>
{
    bool Insert(T t);
    bool Update(T t);
    bool Delete(T t);
    bool Delete(object id);
    IQueryable<T> GetAll();
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
    IQueryable<T> GetBySql(String sql);
    T GetById(object id);
    bool IsExist(object id);
    int GetCount(T t);

    Task<bool> InsertAsync(T t);
    Task<bool> UpdateAsync(T t);
    Task<bool> DeleteAsync(T t);
    Task<bool> DeleteAsync(object id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> GetBySqlAsync(String sql);
    Task<T> GetByIdAsync(object id);
    Task<bool> IsExistAsync(object id);
    Task<int> GetCountAsync(T t);
}
