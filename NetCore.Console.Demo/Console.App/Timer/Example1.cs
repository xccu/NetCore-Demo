namespace ConsoleApplication.TimerExample;

/*
 * read data from stack with a customer frequency   
 */
public class Example1(int maxValue = 100)
{
    private Timer timer;
    private int _value = 0;
    private bool _isFirstReport = true;
    private Stack<int> _valueStack = new Stack<int>();

    public void Run()
    {
        for (int i = 0; i <= maxValue; i++)
        {
            Report(i);
            Thread.Sleep(500);
        }
    }

    void Report(int currentValue) 
    {
        if (_isFirstReport)
        {
            _isFirstReport = false;
            _value = currentValue;
            _ = Task.Factory.StartNew(() =>
            {
                timer = new Timer(new TimerCallback(TimerTask), _valueStack, dueTime: 0, period: 100);
                _valueStack.TryPeek(out int value);
               
                while (value < maxValue)
                {                  
                    Task.Delay(100).Wait();
                    _valueStack.TryPeek(out value);
                }
                timer.Dispose();
                Console.WriteLine($"Timer ended");
            });
        }
        else 
        {
            Console.WriteLine($"Write:{currentValue}");
            _valueStack.Push(currentValue);
        }
    }

    private void TimerTask(object timerState)
    {        
        var stack = timerState as Stack<int>;
        if (stack is not null && stack.TryPeek(out int value))
        {
            if(value<_value)
            Console.WriteLine($"Read:{value}");
        }
        
    }
}
