namespace Orders.Api.Common
{
    /// <summary>
    /// 统一返回结果类(测试使用，并未实际引用)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> where T : class
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 响应信息 
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// 具体数据
        /// </summary>
        public T? Data { get; set; }


        /// <summary>
        /// 成功状态返回
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static ApiResult<T> SuccessResult(T Data)
        {
            return new ApiResult<T>()
            {
                Code = 0,
                Message = "success",
                Data = Data
            };
        }

        /// <summary>
        /// 失败状态返回
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ApiResult<T> FailResult(string msg)
        {
            return new ApiResult<T>()
            {
                Code = 1,
                Message = msg
            };
        }

        /// <summary>
        /// 异常状态返回
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static ApiResult<T> ErrorResult(Exception ex)
        {
            return new ApiResult<T>()
            {
                Code = 1,
                Message = ex.Message
            };
        }

        /// <summary>
        /// 自定义状态返回
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResult<T> MyResult(int code, string message, T data)
        {
            return new ApiResult<T>()
            {
                Code = code,
                Message = message,
                Data = data
            };
        }
    }
}
