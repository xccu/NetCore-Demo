using Hangfire.Job.Attributes;

namespace Hangfire.Job.JobTypes;

public class BasicJobType
{
    public void RunPerMinute()
    {
        Console.WriteLine($"{DateTime.Now.ToString(Constants.Format.timeFormat)}:Run-Per-Minute!");
    }

    //[LogEverything]
    public void RunTargetTime()
    {
        Console.WriteLine($"{DateTime.Now.ToString(Constants.Format.timeFormat)}:Run-Target-Time!");
    }

    public void FireAndForget()
    {
        Console.WriteLine($"{DateTime.Now.ToString(Constants.Format.timeFormat)}:Fire-And-Forget!");
    }

    public void Delayed()
    {
        Console.WriteLine($"{DateTime.Now.ToString(Constants.Format.timeFormat)}:Delayed!");
    }

    public void ContinueWith()
    {
        Console.WriteLine($"{DateTime.Now.ToString(Constants.Format.timeFormat)}:Continue-With");
    }

    [LogEverything]
    public void ThrowException()
    {
        throw new Exception($"{DateTime.Now.ToString(Constants.Format.timeFormat)}:Throw-Exception");
    }
}