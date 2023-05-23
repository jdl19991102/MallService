using FluentValidation;
using Orders.Domain.Exception;

namespace Orders.Api.Middleware
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next; // 用来处理上下文请求 
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // 要么在中间件中处理，要么被传递到下一个中间件中去
                await _next(context);
            }
            catch (Exception ex)
            {
                //捕获异常了在HandleExceptionAsync中处理
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            var code = HttpStatusCode.InternalServerError; //默认使用 500 状态码
            var message = "服务器内部错误";

            if (exception.GetType() == typeof(MyException))
            {
                code = (HttpStatusCode)((MyException)exception).Code;
                message = ((MyException)exception).Message;
            }
            else if (exception is ArgumentException)
            {
                code = HttpStatusCode.BadRequest;
                message = exception.Message;
            }
            else if (exception is InvalidOperationException)
            {
                code = HttpStatusCode.BadRequest; //操作无效异常类型，使用 400 状态码
                message = exception.Message;
            }
            else if (exception is NotImplementedException)
            {
                code = HttpStatusCode.NotImplemented;
                message = exception.Message;
            }
            else if (exception is FileNotFoundException)
            {
                code = HttpStatusCode.NotFound;
                message = exception.Message;
            }
            else if (exception is KeyNotFoundException)
            {
                code = HttpStatusCode.NotFound;
                message = exception.Message;
            }
            else if (exception is TimeoutException)
            {
                code = HttpStatusCode.RequestTimeout;
                message = exception.Message;
            }
            else if (exception is WebException)
            {
                code = HttpStatusCode.BadGateway;
                message = exception.Message;
            }
            else if (exception is UnauthorizedAccessException)
            {
                code = HttpStatusCode.Unauthorized;
                message = "You are not authorized to access this resource.";
            }
            // 测试添加403 没有权限的状态 后续可以在网关中直接进行处理, 这里只是做测试使用
            else if (exception.Message.Contains("Invalid token"))
            {
                code = HttpStatusCode.Forbidden;
                message = exception.Message;
            }
            // FluentValidation参数验证失败
            //返回友好的提示
            else if (exception is ValidationException)
            {
                //var validationException = exception as ValidationException;
                //var error = validationException.Errors.ToList();
                //message = error.ErrorMessage;
                //code = HttpStatusCode.BadRequest;
            }

            _logger.LogError(exception, exception.Message);
            context.Response.ContentType = "application/json;charset=utf-8";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Code = code,
                Message = message,
                Data = ""
            }));
        }

    }
}
