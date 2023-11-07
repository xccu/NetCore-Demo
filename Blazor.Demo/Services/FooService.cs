using Models;

namespace Services;

public class FooService
{
    private Foo _foo;
    public FooService(Foo foo)
    {
        _foo = foo;
    }

    public string GetInfo()
    {
        return $"{_foo.Name}-{_foo.Id}";
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
