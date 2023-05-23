namespace Orders.Domain.Interfaces
{
    /// <summary>
    /// 订单仓储接口
    /// </summary>
    public interface IOrderRepository : IRepository<OrdersInfo>
    {
        Task<IEnumerable<OrdersInfo>> GetAllOrders();

        Task<OrdersInfo> GetOrderDetail(int id);

        void AddNewOrder(OrdersInfo ordersInfo);

        Task<int> SaveChangesAsync();
    }
}
