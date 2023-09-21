using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Entities;
using SqlSugar;
using System.Linq.Expressions;
using System.Xml.Linq;

//并发控制、更新、版本控制
//https://www.donet5.com/Home/Doc?typeId=2399
namespace Demo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ULockController : ControllerBase
    {
        private readonly ULockService _service;
        public ULockController(ULockService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public int CreateUser(ULock uLock)
        {
            var id = _service.CreateAndReturnId(uLock);
            return id;
        }

        [HttpPut("update")]
        public int UpdateUser(ULock uLock)
        {
            var result = _service.Update(uLock);
            return result;
        }

        //乐观锁
        [HttpPut("lock/optimistic")]
        public int OptimisticLock()
        {
            //第一次插入ver=0
            var id = _service.CreateAndReturnId(new() { Name = "oldName", Ver = 0 });

            #region 开始用例
            //会自动更新版本字段更新后数据库将不在是0
            //rows=1 因为数据库ver是0你传的也是0
            int rows = _service.Update(new() { Id = id, Name = "newname", Ver = 0 });


            //rows=0  失败：数据库ver不等于0
            rows = _service.Update(new() { Id = id, Name = "newname2", Ver = 0 });
            #endregion
            return rows;
        }

        //悲观锁
        [HttpPut("lock/pessimistic")]
        public int PessimisticLock()
        {
            for (int i = 0; i < 10; i++)
            {
                Task.Run(() =>
                {
                    try
                    {
                        _service.TransactionTest();
                        Console.WriteLine("Succeed");
                    }
                    catch(Exception ex) 
                    {
                        Console.WriteLine("Failed:"+ex.Message);
                    }
                    
                });
             }
            return 0;
        }

    }
}
