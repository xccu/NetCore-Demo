using Hangfire;
using Hangfire.Job.JobTypes;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Hangfire.Job;

public static class JobService
{
    public static void UseBasicJobType(IServiceProvider provider)
    {
        BasicJobType jobs = new BasicJobType();
  
        var backgroundJob = provider.GetRequiredService<IBackgroundJobClient>();

        var recurringJob = provider.GetRequiredService<IRecurringJobManager>();

        #region basic use
        //支持基于队列的任务处理：任务执行不是同步的，而是放到一个持久化队列中，以便马上把请求控制权返回给调用者。
        //Fire-and-forget jobs are executed only once and almost immediately after creation.
        var jobId = backgroundJob.Enqueue(
            () => jobs.FireAndForget());

        //延迟任务执行：不是马上调用方法，而是设定一个未来时间点再来执行，延迟作业仅执行一次
        //Delayed jobs are executed only once too, but not immediately, after a certain time interval.
        var jobId2 = backgroundJob.Schedule(
            () => jobs.Delayed(),
            TimeSpan.FromDays(1));//一天后执行该任务

        //循环任务执行：一行代码添加重复执行的任务，其内置了常见的时间循环模式，也可基于CRON表达式来设定复杂的模式。【用的比较的多】
        //Recurring jobs fire many times on the specified CRON schedule.
        recurringJob.AddOrUpdate(
            "Run-Per-Minute",
            () => jobs.RunPerMinute(),
            Cron.Minutely); //注意最小单位是分钟

        //不调用方法，仅输出测试
        //cron Expression see:
        //https://blog.csdn.net/study_665/article/details/123506946
        //"0 0 */4 * * ?", 
        recurringJob.AddOrUpdate(
            "Run-Target-Time",
            () => jobs.RunTargetTime(),
            "0 00 14 * * ?",
            new RecurringJobOptions() { TimeZone = TimeZoneInfo.Local });

        //延续性任务执行：类似于.NET中的Task,可以在第一个任务执行完之后紧接着再次执行另外的任务
        backgroundJob.ContinueJobWith(
            jobId,
            () => jobs.ContinueWith());
        #endregion

        #region Obsoleted use
        //recurringJob.AddOrUpdate(
        //    "Run-Target-Time",
        //    () => jobs.RunTargetTime(),
        //    "0 00 14 * * ?",
        //    TimeZoneInfo.Local);

        //backgroundJob.ContinueWith(
        //    jobId,
        //    () => jobs.ContinueWith());
        #endregion

        recurringJob.AddOrUpdate(
            "Throw-Exception",
            () => jobs.ThrowException(),
            "0 00 14 * * ?",
            new RecurringJobOptions() { TimeZone = TimeZoneInfo.Local });

        //backgroundJob.Enqueue(
        //    "q1",
        //    () => jobs.FireAndForget());

    }

    public static void UseEmailSenderJobType(IServiceProvider provider)
    {
        var backgroundJob = provider.GetRequiredService<IBackgroundJobClient>();

        //Passing Dependencies see:
        //https://docs.hangfire.io/en/latest/background-methods/passing-dependencies.html
        backgroundJob.Enqueue<EmailSenderJobType>(x => x.Send( "Hello!"));
    }
}
