using System.Threading.Channels;

namespace ConsoleApplication.ChannelExample;

/*
 * https://learn.microsoft.com/en-us/dotnet/core/extensions/channels#bounded-channels
 */

static class Example
{
    public static void Run()
    {
        Channel<int> myChannel = Channel.CreateUnbounded<int>();
        _ = Task.Factory.StartNew(async () =>
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Wtire:" + i);
                await myChannel.Writer.WriteAsync(i);
                await Task.Delay(1000);
            }

            myChannel.Writer.Complete();
            Console.WriteLine("Write Complete");
        });

        _ = Task.Factory.StartNew(async () =>
        {
            while (await myChannel.Reader.WaitToReadAsync())
            {
                myChannel.Reader.TryRead(out int value);
                Console.WriteLine("Read:" + value);
                await Task.Delay(100);
            }
            Console.WriteLine("Read Complete");
            //await foreach (var item in myChannel.Reader.ReadAllAsync())
            //{
            //    Console.WriteLine("Read:" + item);
            //    await Task.Delay(100);
            //}
            //Console.WriteLine("Read Complete");
        });

    }

    public static async Task RunAsync()
    {
        //var channel = Channel.CreateUnbounded<int>();   //unbounded
        //var channel = Channel.CreateBounded<int>(7);    //bounded

        var myChannel = Channel.CreateUnbounded<int>();
        var producer = new MyProducer(myChannel.Writer);
        var consumer = new MyConsumer(myChannel.Reader);

        for (int i = 0; i < 10; i++)
        {
            producer.Write(i);
        }

        await foreach (var item in consumer.ReadAllAsync())
        {
            Console.WriteLine(item);
        }
    }
}

class MyProducer
{
    private readonly ChannelWriter<int> _channelWriter;

    public MyProducer(ChannelWriter<int> channelWriter)
    {
        _channelWriter = channelWriter;
    }

    public void Write(int value)
    {
        _channelWriter.TryWrite(value);
    }

    public void Complete()
    {
        _channelWriter.Complete();
    }
}

class MyConsumer
{
    private readonly ChannelReader<int> _channelReader;

    public MyConsumer(ChannelReader<int> channelReader)
    {
        _channelReader = channelReader;
    }

    public int Read()
    {
        _channelReader.TryRead(out int value);
        return value;
    }

    public async IAsyncEnumerable<int> ReadAllAsync()
    {
        await foreach (var item in _channelReader.ReadAllAsync())
        {
            yield return item;
        }
    }
}
