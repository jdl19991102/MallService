using FluentValidation.Results;
using MediatR;
using Orders.Domain.Command.Validations;
using Orders.Domain.Events;
using Orders.Domain.Exception;
using Orders.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Command
{
    public class OrderCommandHandler : CommandHandler,
        IRequestHandler<AddNewOrderCommand, bool>
    {

        private readonly IOrderRepository _orderRepository;

        private readonly IMediator _mediator;

        public OrderCommandHandler(IOrderRepository orderRepository, IMediator mediator)
        {
            _orderRepository = orderRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(AddNewOrderCommand request, CancellationToken cancellationToken)
        {
            // 验证
            var validator = new AddNewOrderCommandValidation();
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                HandleErrorMessages(result);
            }

            // 将Command转换成OrdersInfo实体
            var entity = new OrdersInfo
            {
                OrderName = request.OrderName,
                CustomerName = request.CustomerName,
                Price = request.Price
            };
            // 持久化
            _orderRepository.AddNewOrder(entity);

            var flag = await _orderRepository.SaveChangesAsync() > 0;
            if (flag)
            {
                // 发送领域事件
                var orderCreatedEvent = new OrderCreatedEvent(entity.OrderName, entity.CustomerName, entity.Price);
                await _mediator.Publish(orderCreatedEvent);
            }
            return flag;
        }
    }
}
