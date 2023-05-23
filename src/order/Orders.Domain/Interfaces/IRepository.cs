namespace Orders.Domain.Interfaces
{
    /// <summary>
    /// 定义泛型仓储接口，并继承IDisposable，显式释放资源
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        #region 查询
        Task<IEnumerable<TEntity>> SelectListAsync();

        Task<TEntity> SelectOneAsync(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region 增加
        void Insert(TEntity entity);
        #endregion

        #region 修改
        void Update(TEntity entity);
        #endregion

        #region 删除
        void Delete(TEntity entity);
        #endregion
    }
}
