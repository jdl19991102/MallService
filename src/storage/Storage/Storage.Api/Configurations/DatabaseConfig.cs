﻿using Microsoft.EntityFrameworkCore;
using Storage.Api.Data;

namespace Storage.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddDbContext<KnowledgeTestContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //services.AddDbContext<EventStoreSqlContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
