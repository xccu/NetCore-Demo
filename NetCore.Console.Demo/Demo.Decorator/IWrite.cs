using System.Reflection;

namespace Demo.Decorator;

public interface IWrite
{
    void Write();
}


public class Write1 : IWrite
{
    public void Write()
    {
        Console.WriteLine("1");
    }
}

public class Write2 : IWrite
{
    private readonly IWrite _inner;
    private readonly Foo _foo;

    public Write2(IWrite inner,Foo foo)
    {
        _inner = inner;
        _foo = foo;
    }

    public void Write()
    {
        _inner.Write();
        _foo.print();
        Console.WriteLine("2");
    }
}

public class Write3 : IWrite
{
    private readonly IWrite _inner;

    public Write3(IWrite inner)
    {
        _inner = inner;
    }

    public void Write()
    {
        _inner.Write();
        Console.WriteLine("3");
    }
}
