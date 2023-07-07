using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using Storage.Api.Client;
using Storage.Api.Data;
using Storage.Api.DTO;
using System.Threading;

namespace Storage.Api.Service
{
    public class StorageService
    {
        private readonly KnowledgeTestContext _dbContext;

        private readonly IProductClient _productClient;

        private readonly ILogger<StorageService> _logger;

        public StorageService(KnowledgeTestContext dbContext, IProductClient productClient, ILogger<StorageService> logger)
        {
            _dbContext = dbContext;
            _productClient = productClient;
            _logger = logger;
        }

        /// <summary>
        /// 下单扣减库存
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<bool> DecreaseStorage(int productId, int count)
        {
            // 逻辑 1. 调用product 查看商品是否存在，不存在直接返回false
            // 2. 调用storage 查看商品库存是否足够，不够直接返回false
            // 3. 调用storage 减少商品库存

            // 1. 调用product 查看商品是否存在，不存在直接返回false
            var productInfo = await _productClient.GetProductInfo(productId);
            if (string.IsNullOrEmpty(productInfo))
            {
                // 记录错误日志
                _logger.LogError("商品不存在");
                return false;
            }
            // 取出productInfo 中的Data属性的值
            var data = JObject.Parse(productInfo);
            if (data == null || data["Data"].Type == JTokenType.Null)
            {
                _logger.LogError("调用商品服务没有拿到相关的数据");
                return false;
            }
            // 下边这两种写法都一样
            //int storageCount = info["Count"]!.Value<int>();
            //int count1 = (int)info["Count"]!;

            // 2. 调用storage 查看是否还有商品库存，不够直接返回false
            var storageInfo = await GetStorageInfo(productId);
            if (storageInfo <= 0)
            {
                // 记录错误日志
                _logger.LogError($"商品 {data["Data"]["Name"]} 的库存不存在, 对应的商品Id为 {productId} ");
                return false;
            }


            // 判断库存是否足够
            if (storageInfo < count)
            {
                _logger.LogError("商品库存不足");
                return false;
            }

            // 3. 调用storage 减少商品库存
            var flag = await DeceraseStorage1(productId, count);
            if (flag)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据商品id查找相关库存
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<int> GetStorageInfo(int productId)
        {
            // 直接调用数据库查询商品库存
            var result = await _dbContext.StorageInfos.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (result != null)
            {
                return result.Quantity;
            }
            else
            {
                _logger.LogError("productId 为 {0} 没有查到相关的库存", productId);
                return 0;
            }
        }

        /// <summary>
        /// 扣除库存操作
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<bool> DeceraseStorage1(int productId, int count)
        {
            // 直接调用数据库扣除相应库存
            var storageInfo = await _dbContext.StorageInfos.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (storageInfo != null)
            {
                storageInfo.Quantity -= count;
                return await _dbContext.SaveChangesAsync() > 0;
            }
            else
            {
                _logger.LogError("没有查到相关的库存");
                return false;
            }
        }
    }
}
