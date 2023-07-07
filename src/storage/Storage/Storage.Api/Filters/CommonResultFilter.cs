using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Storage.Api.Filters
{
    public class CommonResultFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                objectResult.Value = new
                {
                    Code = 0,
                    Data = objectResult.Value,
                    Message = "请求成功"
                };
            }
        }
    }
}
