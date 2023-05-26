using Hangfire;

namespace RazorPage.Web;

public static class HangfireJobs
{
    public static void Start()
    {
        //支持基于队列的任务处理：任务执行不是同步的，而是放到一个持久化队列中，以便马上把请求控制权返回给调用者。
        //Fire-and-forget jobs are executed only once and almost immediately after creation.
        var jobId = BackgroundJob.Enqueue(() => Console.WriteLine($"{DateTime.Now}:Fire-and-forget!"));

        //延迟任务执行：不是马上调用方法，而是设定一个未来时间点再来执行，延迟作业仅执行一次
        var jobId2 = BackgroundJob.Schedule(() => Console.WriteLine($"{DateTime.Now}:Delayed!"), TimeSpan.FromDays(1));//一天后执行该任务

        //循环任务执行：一行代码添加重复执行的任务，其内置了常见的时间循环模式，也可基于CRON表达式来设定复杂的模式。【用的比较的多】
        RecurringJob.AddOrUpdate(() => Console.WriteLine($"{DateTime.Now}:Recurring!"), Cron.Minutely); //注意最小单位是分钟

        //延续性任务执行：类似于.NET中的Task,可以在第一个任务执行完之后紧接着再次执行另外的任务
        BackgroundJob.ContinueWith(jobId, () => Console.WriteLine($"{DateTime.Now}:Continuation!"));

        //不调用方法，仅输出测试
        RecurringJob.AddOrUpdate($"{DateTime.Now}:Running at 4:00", () => Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")), "0 0 */4 * * ?", TimeZoneInfo.Local);
    }
}
