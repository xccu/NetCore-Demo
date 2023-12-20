using System.Collections.Concurrent;
using System.Runtime.ExceptionServices;

namespace Demo.Exception;

public class Foo
{
    //public void error()
    //{
    //    try
    //    {
    //        test();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //        //throw ex;
    //       // ExceptionDispatchInfo.Capture(ex).Throw();
    //    }

    //}

    /// <summary>
    /// warp as inner exception
    /// </summary>
    /// <param name="context"></param>
    public static void Warp(ExceptionContext context)
    {
        var ex = new InvalidDataException("test", context.exception);
        context.exception = ex;
    }
}
