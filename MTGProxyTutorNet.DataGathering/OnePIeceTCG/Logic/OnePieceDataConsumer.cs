using HtmlAgilityPack;
using MTGProxyTutorNet.Contracts.Exceptions;
using MTGProxyTutorNet.Contracts.Interfaces;
using MTGProxyTutorNet.DataGathering.OnePIeceTCG.Models;
using System.Runtime.Caching;

namespace MTGProxyTutorNet.DataGathering.OnePIeceTCG.Logic
{
    public class OnePieceDataConsumer
    {
        private static HttpClient _client;
        private ILogger _logger;
        private ObjectCache _cache;
        private CacheItemPolicy _cacheItemPolicy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddDays(1) };

        public OnePieceDataConsumer(HttpClient client, ILogger logger)
        {
            _client = client;
            _logger = logger;
            _cache = MemoryCache.Default;
        }

        public async Task<OnePieceTCGSearchResult> GetCardAsync(string url, int msDelay = 0, bool useCache = true)
        {
            HttpResponseMessage response = null;
            OnePieceTCGSearchResult result;

            try
            {
                if (useCache)
                {
                    OnePieceTCGSearchResult cachedValue = getFromCache(url);
                    if (cachedValue != null)
                        return cachedValue;
                }

                await Task.Delay(msDelay);
                response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string html = await response.Content.ReadAsStringAsync();

                result = parseHtml(html);

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

        private OnePieceTCGSearchResult parseHtml(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var cardElements = doc.DocumentNode.SelectNodes("//div[@class='risultati']").SingleOrDefault();
            var cards = new List<OnePieceTCGCard>();

            if (cardElements != null)
            {
                foreach (var card in cardElements.ChildNodes.Where(n => n.HasClass("preview")))
                {
                    var imgUrl = card.Attributes.SingleOrDefault(a => a.Name == "data-href")?.Value;
                    var cardId = card.Descendants().SingleOrDefault(n => n.HasClass("idcarta"))?.InnerText;
                    var cardName = card.Descendants().SingleOrDefault(n => n.HasClass("nomecarta"))?.InnerText;

                    if (!string.IsNullOrWhiteSpace(cardId))
                        cardId = cardId.Trim();

                    var c = new OnePieceTCGCard
                    {
                        CardId = cardId,
                        CardName = cardName,
                        IsAltArt = cards.Any(x => x.CardId == cardId),
                        CardImageRelativeUrl = imgUrl
                    };

                    cards.Add(c);
                }
            }

            return new OnePieceTCGSearchResult
            {
                Data = cards
            };
        }

        private OnePieceTCGSearchResult getFromCache(string key)
        {
            try
            {
                if (_cache.Contains(key))
                    return _cache.Get(key) as OnePieceTCGSearchResult;
                return null;
            }
            catch (Exception ex)
            {
                _logger.Error($"Could not get cache item for url: {key} - Exception: {ex.Message}");
                return null;
            }
        }

        private void addToCache(string key, OnePieceTCGSearchResult obj)
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
