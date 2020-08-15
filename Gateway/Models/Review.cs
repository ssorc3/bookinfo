namespace Reviews.Models
{
    public class Review
    {
        public string Reviewer { get; set; }
        public string Text { get; set; }
        public int? Rating { get; set; }
    }
}