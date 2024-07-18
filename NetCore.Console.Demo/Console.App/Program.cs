
//Console.WriteLine("Hello World");
//Demo.Delegates.ChainDelegateDemo.Run();
//Demo.ExpressionTree.ExpressionTreeDemo.Run();
//Demo.Decorator.DecoratorDemo.Run();
//Demo.Exception.ExceptionDemo.Run();
//Demo.AutoMapper.AutoMapperDemo.Run();
//await Demo.HttpClient.HttpClientDemo.RunAsync();

//DateTime dt = DateTime.Now;
//CultureInfo culture = CultureInfo.GetCultureInfo("en-US");
//string format = culture.DateTimeFormat.ShortDatePattern + " " + culture.DateTimeFormat.ShortTimePattern;
//Console.WriteLine(dt.ToString(format, culture));

//culture = CultureInfo.GetCultureInfo("zh-CN");
//format = culture.DateTimeFormat.ShortDatePattern + " " + culture.DateTimeFormat.ShortTimePattern;
//Console.WriteLine(dt.ToString(format, culture));


//FireAndForgetExceptionExample.Run();
//FireAndForgetExceptionExample.RunAsync().Wait();


#region Cannel Example
var example1 = new ConsoleApplication.ChannelExample.Example1(50);
example1.Run();

//ConsoleApplication.ChannelExample.Example.Run();
#endregion

#region Timer
//ConsoleApplication.TimerExample.Example.Run();

//var example1 = new ConsoleApplication.TimerExample.Example1(10);
//example1.Run();
#endregion

Console.WriteLine("Press any key to continue...");
Console.ReadKey();