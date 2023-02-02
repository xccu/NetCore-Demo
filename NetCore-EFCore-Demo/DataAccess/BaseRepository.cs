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

namespace DataAccess
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly UserContext dbContext;
        public BaseRepository(UserContext dbContext) => this.dbContext = dbContext;
        public  bool Insert(T entity)
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

        public IQueryable<T> GetAll()
        {
            return dbContext.Set<T>();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return dbContext.Set<T>().Where(expression);
        }

        public  T GetById(object id)
        {
            if (id == null)
            {
                return null;
            }
            var result = dbContext.Set<T>().Find(id);
            return result;
        }


        public bool IsExist(object id)
        {
            if (id == null)
            {
                return false;
            }

            return  dbContext.Set<T>().Find(id) != null;
        }

        public int GetCount(T entity)
        {
            return  dbContext.Set<T>().Count();
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

        public IQueryable<T> GetBySql(string sql)
        {
            var list = dbContext.Set<T>(sql);
            return list;
        }
    }     

}
