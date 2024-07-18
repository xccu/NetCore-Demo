using System.IO;
using System.Threading.Channels;

namespace ConsoleApplication.ChannelExample;

/*
 * read data from cannel with a customer frequency   
 */
public class Example1
{   
    Channel<int> _channel = Channel.CreateBounded<int>(new BoundedChannelOptions(1) { FullMode = BoundedChannelFullMode.DropOldest});
    private int _value = 0;
    private bool _isFirstCalling = true;
    private int _maxValue;

    public Example1(int maxValue = 100)
    {
        _maxValue = maxValue;
        //start new thread to read from Channel
        _ = Task.Factory.StartNew(async () =>
        {
            await ReadAsync();
        });
    }

    public void Run()
    {      
        for (int i = 0; i <= _maxValue; i++) 
        {            
            Report(i);
            Thread.Sleep(100);
        }        
    }

    void Report(int latestValue)
    {
        // first calling report 
        if (_isFirstCalling) 
        {
            _value = latestValue;
            _isFirstCalling = false;
            // write into channel directly
            Write(_value);
        }

        // latest value bigger than current one, then write into Channel
        if (latestValue > _value)
        {
            latestValue = latestValue >= _maxValue ? _maxValue : latestValue;
            _value = latestValue;
            Write(_value);

            //if progress completed,then complete Cannel
            if (latestValue == _maxValue)
            {
                _channel.Writer.Complete();
                Console.WriteLine("Wtire Complete");
            }
        }
    }

    void Write(int value)
    {
        Console.WriteLine($"Wtire:{value}");
        _channel.Writer.TryWrite(value);
    }

    async Task ReadAsync() 
    {
        // if channel completed then loop ended
        // WaitToReadAsync() method will block until completed or has new data
        while (await _channel.Reader.WaitToReadAsync())
        {
            //read and push all datas from cannel into stack until empty
            while (_channel.Reader.TryRead(out int value))
            {
                Console.WriteLine($"Read:{value}\r\n");
            }

            //time span
            await Task.Delay(500);
        }
        Console.WriteLine("Read Complete");
    }
}
