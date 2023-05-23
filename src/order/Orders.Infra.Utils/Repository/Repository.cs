namespace Orders.Infra.Utils.Repository
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly KnowledgeTestContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(KnowledgeTestContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> SelectListAsync()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> SelectOneAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.AsNoTracking().SingleOrDefaultAsync(expression);           
        }

        public void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }     
    }
}
