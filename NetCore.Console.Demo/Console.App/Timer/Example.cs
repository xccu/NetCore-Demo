namespace ConsoleApplication.TimerExample;

public static class Example
{
    private static Timer timer;

    public static void Run()
    {
        var timerState = new TimerState { Counter = 0 };

        timer = new Timer(
            callback: new TimerCallback(TimerTask),
            state: timerState,
            dueTime: 1000,
            period: 500);

        while (timerState.Counter <= 10)
        {
            Task.Delay(1000).Wait();
        }

        timer.Dispose();
        Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff}: done.");
    }

    private static void TimerTask(object timerState)
    {        
        var state = timerState as TimerState;
        Interlocked.Increment(ref state.Counter);
        Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff}: starting a new callback. Counter={state.Counter}");
    }

    class TimerState
    {
        public int Counter;
    }
}