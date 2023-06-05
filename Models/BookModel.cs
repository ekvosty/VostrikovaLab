namespace VostrikovaLab.Models
{
    public class BookModel
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int PageCount { get; set; }
    }
}
