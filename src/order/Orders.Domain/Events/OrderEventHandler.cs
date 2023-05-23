using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Events
{
    public class OrderEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly ILogger<OrderEventHandler> _logger;

        public OrderEventHandler(ILogger<OrderEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            // 打印日志模拟发送文件
            _logger.LogError("新增订单成功, 请注意查收邮件哦");
            return Task.CompletedTask;
        }
    }
}
