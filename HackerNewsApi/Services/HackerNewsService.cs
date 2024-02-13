using HackerNewsApi.Dtos;
using Newtonsoft.Json;

namespace HackerNewsApi.Services
{
    public class HackerNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri = "https://hacker-news.firebaseio.com/v0";
       
        public HackerNewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public virtual async Task<IEnumerable<int>> GetBestStoryIdsAsync()
        {
            var response = await _httpClient.GetStringAsync($"{_baseUri}/beststories.json");

            var list =  JsonConvert.DeserializeObject<IEnumerable<int>>(response);

            if (list == null)
            {
                return Enumerable.Empty<int>();
            }
            else
            {
                return list;
            }
        }

        public virtual async Task<HackerNewsStory?> GetStoryDetailsAsync(int id)
        {
            var response = await _httpClient.GetStringAsync($"{_baseUri}/item/{id}.json");
            if (string.IsNullOrEmpty(response))
            {
                return null;
            }
            var story = JsonConvert.DeserializeObject<HackerNewsStory>(response);
            return story;
        }
    }
}
