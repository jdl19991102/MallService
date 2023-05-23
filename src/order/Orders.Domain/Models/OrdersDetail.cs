namespace Orders.Domain.Models
{
    public partial class OrdersDetail
    {
        public int Id { get; set; }
        /// <summary>
        /// 订单表主键
        /// </summary>
        public int? OrdersId { get; set; }
        /// <summary>
        /// 子订单名称
        /// </summary>
        public string? DetailsName { get; set; }
        /// <summary>
        /// 子订单价格
        /// </summary>
        public decimal? DetailsPrice { get; set; }
    }
}
