﻿using Bookify.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Caching
{
    internal class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            byte[]? bytes = await _cache.GetAsync(key, cancellationToken);
            return bytes is null ?default:Desrialize<T>(bytes);
        }

        private static T Desrialize<T>(byte[] bytes)
        {
            return JsonSerializer.Deserialize<T>(bytes)!;
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
           return _cache.RemoveAsync(key, cancellationToken);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            byte[] bytes = Serialize(value);
            return _cache.SetAsync(key,bytes,CacheOptions.Create(expiration),cancellationToken);
        }

        private static byte[] Serialize<T>(T value)
        {
           var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);
            JsonSerializer.Serialize(writer, value);
            return buffer.WrittenSpan.ToArray();
        }
    }

   
}
