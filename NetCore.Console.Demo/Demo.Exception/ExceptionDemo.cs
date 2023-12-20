using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Exception;

public class ExceptionDemo
{
    public static void Run()
    {
        try
        {
            throw new InvalidCastException();
        }
        catch (System.Exception ex)
        {
            var context = new ExceptionContext();
            context.exception = ex;
            try
            {
                Demo.Exception.Foo.Warp(context);
                ExceptionDispatchInfo.Throw(context.exception);
            }
            catch (System.Exception e)
            {

            }
        }
    }
}
