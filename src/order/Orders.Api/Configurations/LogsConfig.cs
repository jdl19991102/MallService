namespace Orders.Api.Configurations
{
    /// <summary>
    /// 配置Serilog 
    /// </summary>
    public static class LogsConfig
    {
        public static void AddLogsConfiguration(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                //设定最小的记录级别
                .MinimumLevel.Information()
                //如果遇到Microsoft命名空间，那么最小记录级别为Information
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                //记录相关上下文信息.配置Enrich.FromLogContext()的目的是为了从日志上下文中获取一些关键信息诸如用户ID或请求ID
                .Enrich.FromLogContext()
                // 记录线程Id. 这里有一个知识点需要注意,如果你想打印的ThreadId是固定的，就不用定义ThreadIdEnricher类
                // 如果你想打印的ThreadId是动态的,就要像官网当中描写的那样去定义ThreadIdEnricher类
                // 不然使用Enrich.WithProperty("ThreadId", Thread.CurrentThread.ManagedThreadId)打印出来的ThreadId都是一样的
                .Enrich.WithThreadId()
                // 有一点要记住, 当想记录的东西是动态变化的时候,就要定义一个类去实现ILogEventEnricher接口
                // 然后在这个类中去实现ILogEventEnricher接口中的Enrich方法
                // 然后在这里使用Enrich.With()方法去调用这个类,这样就可以实现动态记录了,如下所示
                // .Enrich.With(new ThreadNameEnricher())
                // .Enrich.With(new ThreadIdEnricher())
                // 记录线程名字 线程名字默认是.NET ThreadPool Worker ,在此记录参考意义不大，所以可以不用记录
                .Enrich.WithThreadName()
                // 记录一些不变的属性,比如应用名字
                // WithProperty 方法可以添加一些不变的属性，比如应用名字，这样在日志中就可以看到这些属性了
                // 不管是WithProperty 还是 With 所想添加的一些属性, 想要看到就必须要写到日志输出模板中, 否则无法看到记录的相关数据
                .Enrich.WithProperty("Application", "Orders.Api")
                // 输出到控制台
                .WriteTo.Console()
                // 输出到文件 生成周期每天
                .WriteTo.File(
                path: "Logs/logs.txt", // 日志文件路径，这里使用相对路径，相对于程序运行目录
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {NewLine}{Exception}", // 输出模板
                retainedFileCountLimit: 15,           // 保留的文件个数,默认为31个
                encoding: Encoding.UTF8,              // 文件字符编码,默认为UTF-8,这里可写可不写
                rollingInterval: RollingInterval.Day) // 生成周期,默认为Day,这里可写可不写
                .CreateLogger();


            // 使用Serilog作为日志框架，注意这里和.NET 5及之前的版本写法是不太一样的。
            builder.Host.UseSerilog();
        }
    }
}
