using MTGProxyTutorNet.BusinessLogic.Contracts.Interfaces;
using MTGProxyTutorNet.Contracts.Exceptions;
using MTGProxyTutorNet.DataGathering.Contracts.Interfaces;
using Newtonsoft.Json;
using System.Runtime.Caching;

namespace MTGProxyTutorNet.DataGathering.Http
{
	public class WebApiConsumer : IWebApiConsumer
	{
		private static HttpClient _client;
		private ILogger _logger;
		private ObjectCache _cache;
		private CacheItemPolicy _cacheItemPolicy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddDays(1) };

		public WebApiConsumer(HttpClient client, ILogger logger)
		{
			_client = client;
			_logger = logger;
			_cache = MemoryCache.Default;
		}

		public async Task<T> GetAsync<T>(string url, int msDelay = 0, bool useCache = true) where T: class
		{
			HttpResponseMessage response = null;
			T result;

			try
			{
				if (useCache)
				{
					var cachedValue = getFromCache<T>(url);
					if (cachedValue != null)
						return cachedValue;
				}

                await Task.Delay(msDelay);
                response = await _client.GetAsync(url);
				response.EnsureSuccessStatusCode();
				string body = await response.Content.ReadAsStringAsync();

				if(typeof(T) == typeof(string))
				{
					result = body as T;
				}
				else
				{
					result = JsonConvert.DeserializeObject<T>(body);
				}

				if (useCache)
					addToCache(url, result);

				return result;
			}
			catch (Exception ex)
			{
				_logger.Error($"GET Error: {ex.Message}");
				throw new WebApiConsumerException($"GET Error - {ex.Message}", ex, response?.StatusCode);
			}
		}

		public T Get<T>(string url, int msDelay = 0, bool useCache = true) where T : class
		{
			var task = GetAsync<T>(url, msDelay, useCache);
			task.Wait();
			return task.Result;
		}

		public async Task<byte[]> GetBinaryAsync(string url, int msDelay = 0, bool useCache = true)
		{
			HttpResponseMessage response = null;

			try
			{
				if (useCache)
				{
					var cachedValue = getFromCache<byte[]>(url);
					if (cachedValue != null)
						return cachedValue;
				}

				await Task.Delay(msDelay);
				response = await _client.GetAsync(url);
				response.EnsureSuccessStatusCode();
				var binary = await response.Content.ReadAsByteArrayAsync();

				if (useCache)
					addToCache(url, binary);

				return binary;
			}
			catch (Exception ex)
			{
				_logger.Error($"GET Error: {ex.Message}");
				throw new WebApiConsumerException($"GET Error - {ex.Message}", ex, response?.StatusCode);
			}
		}

		public byte[] GetBinary(string url, int msDelay = 0, bool useCache = true)
		{
			var task = GetBinaryAsync(url, msDelay, useCache);
			task.Wait();
			return task.Result;
		}

		private T getFromCache<T>(string key) where T : class
		{
			try
			{
				if (_cache.Contains(key))
					return _cache.Get(key) as T;
				return null;
			}
			catch (Exception ex)
			{
				_logger.Error($"Could not get cache item for url: {key} - Exception: {ex.Message}");
				return null;
			}
		}

		private void addToCache<T>(string key, T obj) where T : class
		{
			try
			{
				var cacheItem = new CacheItem(key, obj);
				if (!_cache.Contains(key))
					_cache.Add(cacheItem, _cacheItemPolicy);
			}
			catch (Exception ex)
			{
				_logger.Error($"Could not add/update cache item for url: {key} - Exception: {ex.Message}");
			}
		}
	}
}
