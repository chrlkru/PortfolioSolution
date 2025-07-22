namespace Shared
{
    public class PetProject
    {
        public int Id { get; set; }
        public string Slug { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string ThumbnailUrl { get; set; } = "";
        public string GitHubUrl { get; set; } = "";
        public string DownloadUrl { get; set; } = "";
        public string Stack { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
