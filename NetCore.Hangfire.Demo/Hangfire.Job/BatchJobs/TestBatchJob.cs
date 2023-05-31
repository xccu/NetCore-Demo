using Hangfire.Job.Attributes;

namespace Hangfire.Job.BatchJobs;

[JobType("Test Batchjob")]
public class TestBatchJob
{
    public Task RunAsync()
    {
        Console.WriteLine("test batch job..");

        return Task.CompletedTask;
    }
}
