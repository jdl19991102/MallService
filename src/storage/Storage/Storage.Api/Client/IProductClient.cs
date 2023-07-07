namespace Storage.Api.Client
{
    public interface IProductClient
    {
        Task<string> GetProductInfo(int productId);
    }
}
