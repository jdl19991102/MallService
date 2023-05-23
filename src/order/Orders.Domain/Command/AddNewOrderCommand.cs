using FluentValidation.Results;
using MediatR;
using Orders.Domain.Command.Validations;

namespace Orders.Domain.Command
{
    public class AddNewOrderCommand : IRequest<bool>
    {
        // 新增订单Command
        public AddNewOrderCommand(string orderName, string customerName, int price)
        {
            OrderName = orderName;
            CustomerName = customerName;
            Price = price;
        }
        // 订单名字
        public string OrderName { get; private set; }
        // 客户名字
        public string CustomerName { get; private set; }
        // 价格
        public int Price { get; private set; }
    }
}
