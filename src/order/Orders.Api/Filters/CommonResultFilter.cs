namespace Orders.Api.Filters
{
    /// <summary>
    /// 过滤器统一处理返回结果
    /// </summary>
    public class CommonResultFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                // 查到的值为 null，返回错误信息
                if (objectResult.Value == null)
                {
                    context.Result = new ObjectResult(new ApiResponse
                    {
                        Code = 1,
                        Message = "未找到资源",
                        Data = ""
                    });
                }
                else
                {
                    // 如果返回结果是 ApiResponse 类型，则不做处理
                    if (objectResult.Value is ApiResponse)
                    {
                        return;
                    }
                    // 返回结果如果是 ApiResponse<T> 类型，则转换为 ApiResponse 类型 
                    // 这一段是增加了这个返回类型，实际上不会返回ApiResponse<T> 类型,只会返回ApiResponse类型或者原样类型返回然后在这里封装
                    if (objectResult.Value.GetType().IsGenericType && objectResult.Value.GetType().GetGenericTypeDefinition() == typeof(ApiResponse<>))
                    {
                        int code = (int)(objectResult.Value.GetType().GetProperty("Code")?.GetValue(objectResult.Value, null));
                        string? message = (string?)(objectResult.Value.GetType().GetProperty("Message")?.GetValue(objectResult.Value, null));
                        var data = objectResult.Value.GetType().GetProperty("Data")?.GetValue(objectResult.Value, null);
                        context.Result = new ObjectResult(new ApiResponse
                        {
                            Code = code,
                            Message = message,
                            Data = data
                        });
                        return;
                    }
                    // 反之，对其进行处理
                    context.Result = new ObjectResult(new ApiResponse
                    {
                        Code = 0,
                        Message = "success",
                        Data = objectResult.Value
                    });
                    //context.Result = new JsonResult(new ApiResponse(0, "success", objectResult.Value));
                }
                //return;
            }
            base.OnActionExecuted(context);
        }
    }
}
