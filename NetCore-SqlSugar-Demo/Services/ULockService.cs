using Services.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class ULockService
{
    ISqlSugarClient _db;
    public ULockService(ISqlSugarClient db)
    {
        _db = db;
    }

    public int CreateAndReturnId(ULock uLock)
    {
        return _db.Insertable(uLock).ExecuteReturnIdentity();
    }

    /// <summary>
    /// 用法1：不抛异常
    /// Ver与数据库字段不同不报错返回0
    /// </summary>
    /// <param name="uLock"></param>
    /// <returns></returns>
    public int Update(ULock uLock)
    {
        var rows = _db.Updateable(uLock).ExecuteCommandWithOptLock();
        return rows;
    }

    /// <summary>
    /// 用法2：抛出异常 
    /// Ver与数据库字段不同直接扔错出误
    /// </summary>
    /// <param name="uLock"></param>
    /// <returns></returns>
    public int UpdateWithException(ULock uLock)
    {
        var rows = _db.Updateable(uLock).ExecuteCommandWithOptLock(true); //加上true就会抛出异常
        return rows;
    }

    public void TransactionTest()
    {
        var db = GetInstance(_db.CurrentConnectionConfig.ConnectionString);
        db.Ado.BeginTran();
        var getAll = db.Queryable<ULock>().TranLock(DbLockType.Error).ToList();
        Thread.Sleep(1000);
        db.Ado.CommitTran();         
    }

    public int Delete(Expression<Func<ULock, bool>> expression)
    {
        //根据表达式删除
        return _db.Deleteable<ULock>().Where(expression).ExecuteCommand();
    }


    private SqlSugarClient GetInstance(string conn)
    {
        SqlSugarClient sqlSugar = new SqlSugarClient(new ConnectionConfig()
        {
            DbType = DbType.SqlServer,
            ConnectionString = conn,
            IsAutoCloseConnection = true,
        });

        return sqlSugar;
    }
}
