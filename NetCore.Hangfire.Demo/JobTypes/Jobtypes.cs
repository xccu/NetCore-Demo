namespace JobTypes;

public class Jobtypes
{
    public string timeFormat = "yyyy-MM-dd HH:mm:ss.fff";

    public void RunPerMinute()
    {
        Console.WriteLine($"{DateTime.Now.ToString(timeFormat)}:Run-Per-Minute!");
    }

    public void RunTargetTime()
    {
        Console.WriteLine($"{DateTime.Now.ToString(timeFormat)}:Run-Target-Time!");
    }

    public void FireAndForget()
    {
        Console.WriteLine($"{DateTime.Now.ToString(timeFormat)}:Fire-and-forget!");
    }

    public void Delayed()
    {
        Console.WriteLine($"{DateTime.Now.ToString(timeFormat)}:Delayed!");
    }

    public void ContinueWith() 
    {
        Console.WriteLine($"{DateTime.Now.ToString(timeFormat)}:ContinueWith");
    }
}