using Orders.Domain.Exception;

namespace Orders.Api.Filters
{
    /// <summary>
    /// 全局异常拦截过滤器
    /// </summary>
    public class GlobalExceptionFilterAsync : IAsyncExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilterAsync> _logger;

        public int Code { get; private set; }
        public string? Message { get; private set; }

        public GlobalExceptionFilterAsync(ILogger<GlobalExceptionFilterAsync> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 重新OnExceptionAsync方法
        /// </summary>
        /// <param name="context">异常信息</param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            // 如果异常没有被处理，则进行处理
            if (context.ExceptionHandled == false)
            {
                // 记录错误信息               
                _logger.LogError(context.Exception, context.Exception.Message);

                //返回异常中自定义的Code状态码
                //以下这两种返回方式都可以
                //Code = context.Exception.GetType().GetProperty("Code")?.GetValue(context.Exception, null) ?? -1,                      
                //Message = context.Exception.GetType().GetProperty("Message")?.GetValue(context.Exception, null) ?? "未知错误",
                //Code = ((Order.MicroService.Api.Filters.MyException)context.Exception).Code,
                //或者直接就是将code返回500，这里均可以自定义
                //code = 500

                if (context.Exception.GetType() == typeof(MyException))
                {
                    Code = ((MyException)context.Exception).Code;
                    Message = ((MyException)context.Exception).Message;
                }
                else
                {
                    Code = 0;
                    Message = context.Exception.Message;
                }

                context.Result = new ContentResult
                {
                    // 这边由于我们做的有异常处理，所以context.HttpContext.Response.StatusCode得到的结果就是200
                    // 我们可以根据业务情况手动设置返回的状态码,到底是设置为500还是200
                    // 这里的话，我们暂时设置为500,这样的话,我们就可以在前端根据状态码来判断是否有异常,效果更明显
                    //StatusCode = context.HttpContext.Response.StatusCode,
                    StatusCode = 500,
                    ContentType = "application/json;charset=utf-8",
                    Content = JsonConvert.SerializeObject(new
                    {
                        Code,
                        Message,
                        Data = ""
                    })
                };

                // 设置为true，表示异常已经被处理了，其它捕获异常的地方就不会再处理了
                context.ExceptionHandled = true;
            }
            return Task.CompletedTask;
        }
    }
}
