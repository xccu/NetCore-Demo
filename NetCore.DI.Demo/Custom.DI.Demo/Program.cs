// See
// https://www.cnblogs.com/artech/p/inside-asp-net-core-6-4.html
// https://github.com/jiangjinnan/InsideAspNet6/tree/main/02/S201


using Custom;
using Custom.DI.Demo;

var root = new Cat();

#region S201
//root.Register<IFoo, Foo>(Lifetime.Transient);
//root.Register<IBar>(_ => new Bar(), Lifetime.Self);
//root.Register<IBaz, Baz>(Lifetime.Root);
//root.Register(typeof(Foo).Assembly);

//var cat1 = root.CreateChild();
//var cat2 = root.CreateChild();

//void GetServices<TService>(Cat cat) where TService : class
//{
//    cat.GetService<TService>();
//    cat.GetService<TService>();
//}

//var foo = cat1.GetService<IFoo>();
////Instance of Foo is created.
////Instance of Foo is created.
////Instance of Bar is created.
////Instance of Baz is created.
////Instance of Qux is created.
//GetServices<IFoo>(cat1);
//GetServices<IBar>(cat1);
//GetServices<IBaz>(cat1);
//GetServices<IQux>(cat1);
//Console.WriteLine();

////Instance of Foo is created.
////Instance of Foo is created.
////Instance of Bar is created.
//GetServices<IFoo>(cat2);
//GetServices<IBar>(cat2);
//GetServices<IBaz>(cat2);
//GetServices<IQux>(cat2);
#endregion

#region
root.Register<IFoo, Foo>(Lifetime.Root);
root.Register<IBar, Bar>(Lifetime.Self);
root.Register<IBaz, Baz>(Lifetime.Transient);

var cat1 = root.CreateChild();
var cat2 = root.CreateChild();

Console.WriteLine("Cat1");
cat1.GetService<IFoo>().print();
cat1.GetService<IFoo>().print();
cat1.GetService<IBar>().print();
cat1.GetService<IBar>().print();
cat1.GetService<IBaz>().print();
cat1.GetService<IBaz>().print();

Console.WriteLine("Cat2");
cat2.GetService<IFoo>().print();
cat2.GetService<IFoo>().print();
cat2.GetService<IBar>().print();
cat2.GetService<IBar>().print();
cat2.GetService<IBaz>().print();
cat2.GetService<IBaz>().print();

#endregion
Console.ReadKey();
