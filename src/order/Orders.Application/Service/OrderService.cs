using FluentValidation.Results;
using MediatR;
using Orders.Domain.Command;

namespace Orders.Application.Service
{
    public class OrderService : IOrderService
    {
        //仓储接口
        private readonly IOrderRepository _orderRepository;
        //AutoMapper        
        private readonly IMapper _mapper;
        // MediatR
        private readonly IMediator _mediator;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IMediator mediator)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<OrdersViewModel>> GetAllOrders()
        {
            var ordersList = await _orderRepository.GetAllOrders();
            var result = _mapper.Map<IEnumerable<OrdersViewModel>>(ordersList);
            return result;
        }

        public async Task<OrdersViewModel> GetOrder(int id)
        {
            var orderInfo = await _orderRepository.GetOrderDetail(id);
            var result = _mapper.Map<OrdersViewModel>(orderInfo);
            return result;
        }

        public async Task<bool> CreateOrder(AddNewOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        public Task<bool> RemoveOrder(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
