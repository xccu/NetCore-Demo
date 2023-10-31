using Models;

namespace Services;

public class BarService
{
    private Bar _bar;
    public BarService(Bar bar)
    {
        _bar = bar;
    }

    public string GetInfo()
    {
        return $"{_bar.Name}-{_bar.Id}";
    }
}
