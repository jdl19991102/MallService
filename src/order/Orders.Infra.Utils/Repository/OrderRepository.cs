namespace Orders.Infra.Utils.Repository
{
    public class OrderRepository : Repository<OrdersInfo>, IOrderRepository
    {
        public OrderRepository(KnowledgeTestContext Db) : base(Db)
        {
        }

        public Task<IEnumerable<OrdersInfo>> GetAllOrders()
        {
            return base.SelectListAsync();
        }

        public void AddNewOrder(OrdersInfo ordersInfo)
        {
            base.Insert(ordersInfo);
        }

        public async Task<OrdersInfo> GetOrderDetail(int id)
        {
            return await base.SelectOneAsync(x => x.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Db.SaveChangesAsync();
        }
    }
}
