using Models;
using System.Text.Json.Serialization;

namespace Services;

public class FooService
{
    private Foo _foo;

    public FooService(Foo foo)
    {
        _foo = foo;
    }

    public Foo GetFoo() => _foo;



    public string GetInfo()
    {
        return $"{_foo.Name}-{_foo.Id}";
    }

    public int AddOne(int num)
    {
        return num+1;
    }

    public async Task<Foo> ThrowAsync()
    {
        int n = 0;
        return await Task.FromResult(new Foo
        {            
            Number = 10 / n,
            Name = "a"
        }); 
    }
}
