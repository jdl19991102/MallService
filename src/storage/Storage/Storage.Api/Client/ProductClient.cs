using Polly;
using RestSharp;
using System.Runtime.InteropServices;

namespace Storage.Api.Client
{
    /// <summary>
    /// Product Client
    /// </summary>
    public class ProductClient : IProductClient, IDisposable
    {
        private readonly RestClient _restClient;

        // 引入日志
        private readonly ILogger<ProductClient> _logger;

        public ProductClient(ILogger<ProductClient> logger)
        {
            _restClient = new RestClient("https://localhost:7017");
            _logger = logger;
        }

        /// <summary>
        /// 调用Product获取产品详情
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<string> GetProductInfo(int productId)
        {
            var request = new RestRequest("Products/GetProductById/{id}");
            request.AddUrlSegment("id", productId);

            // 定义一个fallback 的policy,使用异步方法FallbackAsync,并且记录错误日志
            var fallbackPolicy = Policy<string>.Handle<Exception>().FallbackAsync("", async exception =>
            {
                _logger.LogError("Product接口服务异常");
                await Task.CompletedTask;
            });

            var response = await fallbackPolicy.ExecuteAsync(async () =>
            {
                var response = await _restClient.ExecuteAsync(request);
                _logger.LogInformation($"调用Product服务的url为：{response.ResponseUri}");
                if (response.IsSuccessful)
                {
                    return response.Content;
                }
                else
                {
                    _logger.LogError("调用Product服务失败");
                    return "";
                }
            });

            return response;
        }

        public void Dispose()
        {
            _restClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

