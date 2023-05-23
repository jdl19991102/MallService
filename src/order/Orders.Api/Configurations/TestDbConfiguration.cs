namespace Orders.Api.Configurations
{
    public static class TestDbConfiguration
    {
        // 在应用程序启动的时候,运行一段查询语句,缓解EFCore 第一次加载的时候比较慢的问题
        public static void Preload()
        {
            using (var context = new KnowledgeTestContext())
            {
                context.OrdersInfos.FirstOrDefault();
            }
        }

        public static void WarmUp(this IServiceCollection services)
        {
            using var dbContext = new KnowledgeTestContext();
            dbContext.Database.ExecuteSqlRaw("SELECT 1");
        }
    }
}
