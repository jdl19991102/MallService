using System.Runtime.Serialization;

namespace Orders.Domain.Exception
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    [Serializable]
    public class MyException : System.Exception
    {
        public MyException()
        {
        }

        public MyException(string message) : base(message)
        {
        }

        public MyException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public MyException(int code, string message) : base(message)
        {
            Code = code;
        }

        public MyException(int code, string message, System.Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

        protected MyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
        public int Code { get; set; }

    }
}
