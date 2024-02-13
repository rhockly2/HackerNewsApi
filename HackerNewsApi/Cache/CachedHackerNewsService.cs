using HackerNewsApi.Dtos;
using HackerNewsApi.Services;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNewsApi.Cache
{
    public class CachedHackerNewsService : HackerNewsService
    {
        private readonly IMemoryCache _cache;
        private readonly int _expires = 60;
        public CachedHackerNewsService(HttpClient httpClient, IMemoryCache cache)
            : base(httpClient)
        {
            _cache = cache;
        }

        public override async Task<IEnumerable<int>> GetBestStoryIdsAsync()
        {
            return await _cache.GetOrCreateAsync("best_story_ids", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_expires);
                return await base.GetBestStoryIdsAsync();
            });
        }

        public override async Task<HackerNewsStory?> GetStoryDetailsAsync(int id)
        {
            return await _cache.GetOrCreateAsync($"story_{id}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_expires);
                return await base.GetStoryDetailsAsync(id);
            });
        }
    }
}
