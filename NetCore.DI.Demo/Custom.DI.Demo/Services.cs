using Custom;

namespace Custom.DI.Demo;

public interface IFoo { public void print(); }
public interface IBar { public void print(); }
public interface IBaz { public void print(); }
public interface IQux { public void print(); }
public interface IFoobar<T1, T2> { public void print(); }

public class Foo : Base, IFoo
{
    public void print()=> Console.WriteLine($"Instance {GetType().Name}:{ID}");
}

public class Bar : Base, IBar
{
    public void print() => Console.WriteLine($"Instance {GetType().Name}:{ID}");
}

public class Baz : Base, IBaz
{
    public void print() => Console.WriteLine($"Instance {GetType().Name}:{ID}");
}

[MapTo(typeof(IQux), Lifetime.Root)]
public class Qux : Base, IQux
{
    public void print() => Console.WriteLine($"Instance {GetType().Name}:{ID}");
}

public class Foobar<T1, T2> : IFoobar<T1, T2>
{
    public T1 Foo { get; }
    public T2 Bar { get; }
    public string ID = Guid.NewGuid().ToString();

    public Foobar(T1 foo, T2 bar)
    {
        Foo = foo;
        Bar = bar;
    }
    
    public void print() => Console.WriteLine($"Instance {GetType().Name}:{ID}");

}