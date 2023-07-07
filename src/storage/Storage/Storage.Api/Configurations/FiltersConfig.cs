using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Storage.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Storage.Api.Configurations
{
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

            }).AddNewtonsoftJson(options =>
            {
                // 不使用驼峰，返回：UserName
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();             
                //忽略NULL值
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //日期序列化格式
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;//指定如何处理日期和时间的时区。我们将其设置为本地时间，即上海时间。
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";//指定日期的格式。
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                // 自定义验证失败返回结果
                options.InvalidModelStateResponseFactory = context =>
                {
                    var result = new
                    {
                        Code = 1,
                        Data = context.ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)),
                        Message = "参数错误"
                    };
                    return new BadRequestObjectResult(result);
                };
            });


            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.ReferenceHandler = null;
                // 序列化时间格式 需要自定义一个时间converter
                //options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonConverter());
            });


            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.ReferenceHandler = null;
                // 序列化时间格式
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                //options.JsonSerializerOptions.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                //options.JsonSerializerOptions.Converters.Add(new Newtonsoft.Json.Converters.IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });                 
            });
        }
    }
}
