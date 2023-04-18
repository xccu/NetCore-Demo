using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Console.Demo;

public delegate void NextChainDelegate(string data);

public delegate NextChainDelegate ChainDelegate(NextChainDelegate next);

public static class DelegateHandler
{
    public static ChainDelegate CombineHandler(ChainDelegate[] handlers)
    {
        return next =>
        {
            foreach (var handler in handlers.Reverse())
            {
                next = handler(next);
            }
            return next;
        };
    }
}
