using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Command.Validations
{
    public class AddNewOrderCommandValidation : AbstractValidator<AddNewOrderCommand>
    {
        public AddNewOrderCommandValidation()
        {
            RuleFor(c => c.OrderName)
                .NotEmpty().WithMessage("订单名字不能为空")
                .Length(2, 10).WithMessage("订单名字长度必须在2到10之间");
            RuleFor(c => c.CustomerName)
                .NotEmpty().WithMessage("客户名字不能为空")
                .Length(2, 10).WithMessage("客户名字长度必须在2到10之间");
            RuleFor(c => c.Price)
                .NotEmpty().WithMessage("价格不能为空")
                .GreaterThan(10).WithMessage("价格必须大于10");
        }
    }
}
