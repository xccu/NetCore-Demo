using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Delegates;

public class ChainDelegateDemo
{

    static ChainDelegate handler1 = next => (
    str =>
    {
        Console.WriteLine("handler1:" + str);
        next(str);
    });

    static ChainDelegate handler2 = next => (
    str =>
    {
        Console.WriteLine("handler2:" + str);
        next(str);
    });

    static ChainDelegate handler3 = next => (
    str =>
    {
        Console.WriteLine("handler3:" + str);
        next(str);
    });

    public static void Run()
    {


        NextDelegateTest();

        NextChainDelegate next = i => Console.WriteLine("test1:" + i);

        var del = DelegateHandler.CombineHandler(new ChainDelegate[2] { handler1, handler2 });

        next = del(next);
        next = handler3(next);
        next("hello");

        Console.ReadKey();
    }

    private static void NextDelegateTest()
    {
        NextChainDelegate next = i => Console.WriteLine("test1:" + i);
        next = handler1(next);
        next = handler2(next);
        next("hello");
    }


    private static void ActionTest()
    {
        Action<int> action;
        action = a => Console.WriteLine("input:" + a);
        action(1);
        action.Invoke(2);
    }

    private static void FunctionTest()
    {
        Func<int, int> fun1;
        fun1 = a => { return a + 1; };
        var result = fun1.Invoke(1);
        Console.WriteLine("input:" + result);
    }

    public delegate string PrintDelegate(int x);

    private static void DelegateTest()
    {
        PrintDelegate del = i => "i=" + i;
        var str = del(1);
    }

}