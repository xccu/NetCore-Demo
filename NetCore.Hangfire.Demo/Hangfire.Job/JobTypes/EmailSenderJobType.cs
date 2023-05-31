using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangfire.Job.JobTypes;

public class EmailSenderJobType
{
    public string timeFormat = "yyyy-MM-dd HH:mm:ss.fff";

    public void Send(string message)
    {
        Console.WriteLine($"{DateTime.Now.ToString(timeFormat)}:Send:{message}");
    }
}
