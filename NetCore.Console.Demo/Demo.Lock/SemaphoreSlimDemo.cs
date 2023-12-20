namespace Demo.Lock;

public class SemaphoreSlimDemo
{
    private SemaphoreSlim _asyncLock = new(1);

    private static List<int> list;

    public static void Run()
    {
        var demo = new SemaphoreSlimDemo();
        demo.GetList();
        demo.GetList();
    }

    public List<int> GetList()
    {
        try 
        {
            if (list is not null)
                return list;

            _asyncLock.Wait();
            Initial();
            return list;
        }
        finally 
        {
            _asyncLock.Release(); 
        }        
    }

    private void Initial()
    {
        list = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i);
        }
    }
}