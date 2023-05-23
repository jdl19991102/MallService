namespace Orders.Domain.Models
{
    public partial class OrdersInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 订单名字
        /// </summary>
        public string? OrderName { get; set; }
        /// <summary>
        /// 测试名字你猜
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// 订单价格
        /// </summary>
        public decimal? Price { get; set; }
    }
}
