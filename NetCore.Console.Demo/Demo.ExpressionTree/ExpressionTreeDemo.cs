using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ExpressionTree;

public class ExpressionTreeDemo
{
    public static void Run()
    {
        var FooType = typeof(Foo);
        var nameParameter = Expression.Parameter(typeof(string));
        var idParameter = Expression.Parameter(typeof(int));

        var constructor = FooType.GetConstructor(new[] { typeof(int),typeof(string) });

        var newFooExpression = Expression.New(
            constructor: constructor,
            arguments: new[] { idParameter, nameParameter });

        var result = Expression.Lambda<Func<int, string, Foo>>(
            body: newFooExpression,
            parameters: new[] { idParameter, nameParameter }).Compile();

        var foo = result.Invoke(1,"foo1");
        foo.print();
    }
}
