using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CacheDemo.Extensions.StartupExtensions
{
    public static class P01_IMemoryCacheDemoExtension
    {
        public static void AddMemoryCacheConfigureServices(this IServiceCollection services)
        {
            services.AddMemoryCache(options =>
            {
                //options.Clock = new SystemClock();
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(10);//缓存过期时间
                options.SizeLimit = 1024;
                options.CompactionPercentage = 0.02d;
            });
        }

        public static void UseMemoryCacheConfigure(this IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
