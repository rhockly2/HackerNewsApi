namespace HackerNewsApi.Dtos
{
    public class HackerNewsStory
    {
        public string? By { get; set; }
        public int Descendants { get; set; }
        public int Id { get; set; }
        public List<int> Kids { get; set; } = new List<int>();
        public long Time { get; set; }
        public string? Type { get; set; }
        public string? Url { get; set; }
        public string? Title { get; set; }
        public int Score { get; set; }

        public HackerNewsStoryDto MapToDto()
        {
            var dateTimeOffsetSeconds = DateTimeOffset.FromUnixTimeSeconds(Time);
            return new HackerNewsStoryDto
            {
                Title = Title,
                Uri = Url,
                PostedBy = By,
                Time = dateTimeOffsetSeconds.LocalDateTime,
                Score = Score,
                CommentCount = Kids.Count
            };
        }
    }
}
