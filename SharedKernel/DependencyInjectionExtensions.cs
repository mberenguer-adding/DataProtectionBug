using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace SharedKernel;

public static class DependencyInjectionExtensions
{
    public static async Task<WebApplicationBuilder> AddDataProtection(this WebApplicationBuilder builder)
    {
        var redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379");
        builder.Services.AddDataProtection()
            .SetApplicationName("DataProtectionBug")
            .PersistKeysToStackExchangeRedis(redis, new RedisKey("DataProtectionBug"));
        return builder;
    }
}