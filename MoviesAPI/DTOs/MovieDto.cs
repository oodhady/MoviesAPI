namespace MoviesAPI.DTOs
{
    public class MovieDto
    {
        [MaxLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        [MaxLength(2500)]
        public string StrongLine { get; set; }
        [Range(0d, 10d)]
        public double Rate { get; set; }
        public IFormFile? Poster { get; set; }

        public byte GenraId { get; set; }
    }
}
