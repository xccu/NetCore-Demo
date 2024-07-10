namespace ConsoleApplication.FireAndForget;

public class FireAndForgetExceptionExample
{
    public static void Run()
    {
        //SafeFireAndForget(SomeAsyncWork);
        SafeFireAndForget(() => 
        {
            throw new InvalidOperationException("Simulated exception");
        });
        Console.WriteLine("Main method completed. Exceptions (if any) will be handled asynchronously.");
    }

    public static async Task RunAsync()
    {
        //await FireAndForget(() =>
        //{
        //    //await Task.Delay(1000);
        //    throw new InvalidOperationException("Simulated exception");
        //});
        await FireAndForgetAsync(async () =>
        {
            await Task.Delay(1000);
            throw new InvalidOperationException("Simulated exception");
        });
        Console.WriteLine("Main method completed. Exceptions (if any) will be handled asynchronously.");

        Task.CompletedTask.Wait();
    }

    public static async Task SomeAsyncWork()
    {
        // 模拟一些异步工作
        await Task.Delay(100);
        throw new InvalidOperationException("Simulated exception");
    }

    public static async Task FireAndForget(Action action)
    {
        try
        {
            await Task.CompletedTask;
            action.Invoke();
        }
        catch (Exception ex)
        {
            // 这里可以记录异常
            Console.WriteLine("Exception caught inside FireAndForgetAsync: " + ex.Message);
        }
    }

    public static void SafeFireAndForget(Func<Task> asyncAction)
    {
        _ = FireAndForgetAsync(asyncAction).ContinueWith(
            task =>
            {
                if (task.IsFaulted)
                {
                    // 异常处理
                    Console.WriteLine("Exception caught: " + task.Exception.InnerExceptions);
                }
            }, TaskContinuationOptions.OnlyOnFaulted);
    }
 
    public static async Task FireAndForgetAsync(Func<Task> action)
    {
        try
        {
            await action.Invoke();
        }
        catch (Exception ex)
        {
            // 这里可以记录异常
            Console.WriteLine("Exception caught inside FireAndForgetAsync: " + ex.Message);
        }
    }
}