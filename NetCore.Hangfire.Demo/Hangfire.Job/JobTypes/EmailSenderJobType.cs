using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangfire.Job.JobTypes;

public class EmailSenderJobType
{
    public void Send(string message)
    {
        Console.WriteLine($"{DateTime.Now.ToString(Constants.Format.timeFormat)}:Send:{message}");
    }
}
