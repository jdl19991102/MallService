namespace Orders.Api.Configurations
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc
            //});
        }
    }
}
