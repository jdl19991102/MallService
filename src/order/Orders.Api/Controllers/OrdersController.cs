using FluentValidation.Results;
using Orders.Domain.Command;

namespace Orders.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// 获取全部订单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<OrdersViewModel>> GetAllOrders()
        {
            var result = await _orderService.GetAllOrders();
            return result;
        }

        /// <summary>
        /// 根据订单Id获取单个订单数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<OrdersViewModel> GetOrder(int id)
        {
            var result = await _orderService.GetOrder(id);
            return result;
        }

        /// <summary>
        /// 新增一条订单数据
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateOrder([FromBody] AddNewOrderCommand command)
        {
            var result = await _orderService.CreateOrder(command);
            return result;
        }
    }
}
