using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Contracts.Interfaces;
using Orders.Application.Service;
using Orders.Domain.Command;
using Orders.Domain.Interfaces;
using Orders.Infra.Utils.Repository;

namespace Orders.Infra.IoC
{
    /// <summary>
    /// 单写一层用来添加依赖项，从展示层 Presentation 中隔离
    /// </summary>
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<IOrderService, OrderService>();

            // Infra - Data
            services.AddScoped<IOrderRepository, OrderRepository>();

            //Domain - Commands
            services.AddScoped<IRequestHandler<AddNewOrderCommand, bool>, OrderCommandHandler>();
        }
    }
}
