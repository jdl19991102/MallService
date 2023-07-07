using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Storage.Api.Service;

namespace Storage.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StorageController : ControllerBase
    {

        private readonly StorageService _storageService;

        public StorageController(StorageService storageService)
        {
            _storageService = storageService;
        }



        [HttpPost]
        public async Task<IActionResult> DecreaseStock(int productId, int count)
        {
            var result = await _storageService.DecreaseStorage(productId, count);
            return Ok(result);
        }

        [HttpGet]
        public async Task<int> GetStorageInfo(int productId)
        {
            var result = await _storageService.GetStorageInfo(productId);
            return result;
        }

        [HttpPost]
        public async Task<bool> DeceraseStorage(int productId, int count)
        {
            var result = await _storageService.DeceraseStorage1(productId, count);
            return result;
        }
    }
}
