namespace HackerNewsApi.Dtos
{
   public class HackerNewsStoryDto
    {
        public string? Title { get; set; }
        public string? Uri { get; set; }
        public string? PostedBy { get; set; }
        public DateTime Time { get; set; }
        public int Score { get; set; }
        public long CommentCount { get; set; }
    }
}
