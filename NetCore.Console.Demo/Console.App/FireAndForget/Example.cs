namespace ConsoleApplication.FireAndForgetExample;

public class Example
{
    private string Format 
    {
        get
        {
            return "[" + DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss.fff") + "]:{0}";
        }
    }

    public void Run()
    {
        Console.WriteLine(string.Format(Format, "Main method started."));
        Log("FireAndForget test");
        Console.WriteLine(string.Format(Format,"Main method completed."));
    }

    //exception will not catched
    public void Log(string message)
    {
        LogAsync(message,true).ContinueWith((task) =>
        {
            if (task.IsFaulted)
            {
                Console.WriteLine(task.Exception.InnerException);
                Console.WriteLine(string.Format(Format, task.Exception.InnerException.Message) );
            }
        });
    }

    //exception will not catched
    public void LogWithoutCatching(string message)
    {
        try 
        {
            LogAsync(message, true);
        }
        catch(Exception ex) 
        {           
            Console.WriteLine(ex);
            Console.WriteLine(string.Format(Format, ex.Message));
        }
        
    }

    public async Task LogAsync(string message, bool isThrow)
    {
        // 模拟一些异步工作
        await Task.Delay(1000);
        Console.WriteLine(string.Format(Format, message));
        if (isThrow) throw new InvalidOperationException("Test exception");       
    }
}