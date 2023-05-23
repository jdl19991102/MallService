using Orders.Domain.Command;
using FluentValidation.Results;

namespace Orders.Application.Contracts.Interfaces
{
    /// <summary>
    /// 定义 IOrderService 服务接口
    /// 并继承IDisposable，显式释放资源
    /// 注意这里我们使用的对象，是视图对象模型
    /// </summary>
    public interface IOrderService : IDisposable
    {
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<OrdersViewModel>> GetAllOrders();
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <returns></returns>
        Task<OrdersViewModel> GetOrder(int id);
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="orderViewModel"></param>
        /// <returns></returns>
        Task<bool> CreateOrder(AddNewOrderCommand command);
        /// <summary>
        /// 更新订单
        /// </summary>
        /// <param name="orderViewModel"></param>
        /// <returns></returns>
        //Task<OrderViewModel> UpdateOrder(OrderViewModel orderViewModel);

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveOrder(Guid id);
        
    }
}
