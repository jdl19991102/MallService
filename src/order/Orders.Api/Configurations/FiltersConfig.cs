namespace Orders.Api.Configurations
{
    /// <summary>
    /// 过滤器配置类
    /// </summary>
    public static class FiltersConfig
    {
        public static void AddFiltersConfiguration(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddControllers(options =>
            {
                // 添加全局过滤器
                options.Filters.Add(typeof(CommonResultFilter));
                options.Filters.Add(typeof(GlobalExceptionFilterAsync));
            });
        }
    }
}
