using HackerNewsApi.Cache;
using HackerNewsApi.Dtos;
using HackerNewsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HackerNewsController : ControllerBase
    {
        private readonly HackerNewsService _hackerNewsService;

        public HackerNewsController(CachedHackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet("beststories")]
        public async Task<IActionResult> GetBestStories(int numberOfStories)
        {
            var storyIds = await _hackerNewsService.GetBestStoryIdsAsync();
            var tasks = storyIds.Take(numberOfStories).Select(id => _hackerNewsService.GetStoryDetailsAsync(id));
            var stories = await Task.WhenAll(tasks);
           
            HackerNewsStory[] storiesNotNull = stories.Where(item => item != null).Select(item => item!).ToArray();
            var sortedStories = storiesNotNull.OrderByDescending(story => story.Score);

            var hackerNewsStoryDtos = new List<HackerNewsStoryDto>();
            foreach (var story in sortedStories)
            {
                hackerNewsStoryDtos.Add(story.MapToDto());
            }

            return Ok(hackerNewsStoryDtos);
        }
    }
}