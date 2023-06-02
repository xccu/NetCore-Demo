using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangfire.Job.JobTypes;

public class QueueJobType
{
    public void RunInDefault()
    {
        Thread.Sleep(1000);
        Console.WriteLine($"{DateTime.Now.ToString(Constants.Format.timeFormat)}:Run-In-Default");
    }

    [Queue("alpha")]
    public void RunInAlpha()
    {
        Thread.Sleep(1000);
        Console.WriteLine($"{DateTime.Now.ToString(Constants.Format.timeFormat)}:Run-In-Alpha");
    }

    [Queue("alpha")]
    public void RunInAlpha2()
    {
        Thread.Sleep(1000);
        Console.WriteLine($"{DateTime.Now.ToString(Constants.Format.timeFormat)}:Run-In-Alpha-2");
    }

    [Queue("beta")]
    public void RunInBeta()
    {
        Thread.Sleep(1000);
        Console.WriteLine($"{DateTime.Now.ToString(Constants.Format.timeFormat)}:Run-In-Beta");
    }

    [Queue("beta")]
    public void RunInBeta2()
    {
        Thread.Sleep(1000);
        Console.WriteLine($"{DateTime.Now.ToString(Constants.Format.timeFormat)}:Run-In-Beta-2");
    }

    [Queue("beta")]
    public void RunInBeta3()
    {
        Thread.Sleep(1000);
        Console.WriteLine($"{DateTime.Now.ToString(Constants.Format.timeFormat)}:Run-In-Beta-3");
    }

    public void ContinueWith()
    {
        Console.WriteLine($"{DateTime.Now.ToString(Constants.Format.timeFormat)}:Continue-With");
    }
}
