using Base.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext dbContext;
        public BaseRepository(DbContext dbContext) => this.dbContext = dbContext;
        public bool Insert(T entity)
        {
            bool bRet = false;

            if (entity == null)
            {
                return bRet;
            }

            //bRet = IsEntityValid(entity);
            //if (!bRet)
            //{
            //    return bRet;
            //}

            dbContext.Set<T>().Add(entity);
            return dbContext.SaveChanges() > 0;
        }

        public async Task<bool> InsertAsync(T entity)
        {
            if (entity == null)
            {
                return false;
            }

            dbContext.Set<T>().Add(entity);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public bool Update(T entity)
        {
            bool bRet = false;

            if (entity == null)
            {
                return bRet;
            }

            bRet = IsEntityTracked(entity);
            if (!bRet)
            {
                return bRet;
            }

            bRet = IsEntityValid(entity);
            if (!bRet)
            {
                return bRet;
            }

            dbContext.Set<T>().Update(entity);
            return  dbContext.SaveChanges() > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            bool bRet = false;

            if (entity == null)
            {
                return bRet;
            }

            bRet = IsEntityTracked(entity);
            if (!bRet)
            {
                return bRet;
            }

            bRet = IsEntityValid(entity);
            if (!bRet)
            {
                return bRet;
            }

            dbContext.Set<T>().Update(entity);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public bool Delete(T entity)
        {
            bool bRet = false;

            if (entity == null)
            {
                return bRet;
            }

            bRet = IsEntityTracked(entity);
            if (!bRet)
            {
                return bRet;
            }

            bRet = IsEntityValid(entity);
            if (!bRet)
            {
                return bRet;
            }

            dbContext.Set<T>().Remove(entity);
            return  dbContext.SaveChanges() > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            bool bRet = false;

            if (entity == null)
            {
                return bRet;
            }

            bRet = IsEntityTracked(entity);
            if (!bRet)
            {
                return bRet;
            }

            bRet = IsEntityValid(entity);
            if (!bRet)
            {
                return bRet;
            }

            dbContext.Set<T>().Remove(entity);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public bool Delete(object id)
        {
            var entity = GetById(id);
            return Delete(entity);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = GetById(id);
            return await DeleteAsync(entity);
        }

        public IQueryable<T> GetAll()
        {
            return dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {            
            return dbContext.Set<T>().Where(expression);
        }

        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await dbContext.Set<T>().Where(expression).ToListAsync() ;
        }

        public T GetById(object id)
        {
            if (id == null)
            {
                return null;
            }
            var result = dbContext.Set<T>().Find(id);
            return result;
        }

        public async Task<T> GetByIdAsync(object id)
        {
            if (id == null)
            {
                return null;
            }
            var result = dbContext.Set<T>().FindAsync(id);
            return await result;
        }

        public IQueryable<T> GetBySql(string sql)
        {
            var list = dbContext.Set<T>(sql);
            return list;
        }

        public async Task<IEnumerable<T>> GetBySqlAsync(string sql)
        {
            return await dbContext.Set<T>(sql).ToListAsync();
        }    

        public bool IsExist(object id)
        {
            if (id == null)
            {
                return false;
            }

            return  dbContext.Set<T>().Find(id) != null;
        }

        public async Task<bool> IsExistAsync(object id)
        {
            if (id == null)
            {
                return false;
            }

            return await dbContext.Set<T>().FindAsync(id) != null;
        }

        public int GetCount(T entity)
        {
            return  dbContext.Set<T>().Count();
        }

        public async Task<int> GetCountAsync(T entity)
        {
            return await dbContext.Set<T>().CountAsync();
        }

        private bool IsEntityValid(T entity)
        {
            //判断entity是否是DbContext的Model
            IEntityType entityType = dbContext.Model.FindEntityType(typeof(T));
            if (entityType == null)
            {
                return false;
            }

            //获取主键值名称
            string keyName = entityType.FindPrimaryKey().Properties.Select(p => p.Name).FirstOrDefault();
            if (string.IsNullOrEmpty(keyName))
            {
                return false;
            }

            //获取主键类型
            Type keyType = entityType.FindPrimaryKey().Properties.Select(p => p.ClrType).FirstOrDefault();
            if (keyType == null)
            {
                return false;
            }

            //获取主键值类型的默认值
            object keyDefaultValue = keyType.IsValueType ? Activator.CreateInstance(keyType) : null;

            //获取当前主键值
            object keyValue = entity.GetType().GetProperty(keyName).GetValue(entity, null);

            if (keyDefaultValue.Equals(keyValue))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsEntityTracked(T entity)
        {
            EntityEntry<T> trackedEntity = dbContext.ChangeTracker.Entries<T>().FirstOrDefault(o => o.Entity == entity);
            if (trackedEntity == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }     

}
