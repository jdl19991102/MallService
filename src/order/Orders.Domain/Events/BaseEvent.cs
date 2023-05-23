using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Events
{
    /// <summary>
    /// 事件模型，抽象基类，继承 INotification
    /// </summary>
    public abstract class BaseEvent : INotification
    {
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime Timestamp { get; private set; }
        protected BaseEvent()
        {
            Timestamp = DateTime.Now;
        }
    }
}
