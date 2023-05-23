namespace Orders.Api.Common
{
    /// <summary>
    /// 统一定义返回结果(测试使用，并未实际引用)
    /// </summary>
    public class ApiResponse
    {
        public ApiResponse()
        {
        }

        public ApiResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public ApiResponse(int code, string message, object data)
        {
            Code = code;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// 返回码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object? Data { get; set; }
    }


    public class ApiResponse<T> where T : class
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
