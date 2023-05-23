using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Events
{
    public class OrderCreatedEvent : BaseEvent
    {
        public string OrderName { get; set; }

        public string CustomerName { get; set; }

        public decimal? Price { get; set; }

        public OrderCreatedEvent(string orderName, string customerName, decimal? price)
        {
            OrderName = orderName;
            CustomerName = customerName;
            Price = price;
        }
    }
}
